using Core;
using Core.Properties;
using Core.Utilities;
using DevExpress.Web.Mvc;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class OrderController : BaseController
    {
        #region Grid

        [Route("orders", Name = "Orders")]
        public ActionResult Index()
        {
            var model = new OrderViewModel
            {
                GridViewModel = GetOrderGridViewModel(),
                AddNewOrderUrl = Url.RouteUrl("OrdersAdd"),
            };

            return View(model);
        }

        [Route("orders/grid", Name = "OrderGrid")]
        public ActionResult OrderGrid()
        {
            return PartialView("_OrderGrid", GetOrderGridViewModel());
        }

        private OrderGridViewModel GetOrderGridViewModel()
        {
            var orders = UserItem.Role.Code == RoleCode.ADMIN ? 
                UnitOfWork.OrderRepository.GetAll().Include(o => o.OrderDetails).OrderByDescending(o => o.CreateTime).AsNoTracking().ToList() :
                UnitOfWork.OrderRepository.GetAll().Include(o => o.OrderDetails).Where(o => o.UserID == UserItem.ID).OrderByDescending(o => o.CreateTime).AsNoTracking().ToList();
            return new OrderGridViewModel
            {
                ListUrl = Url.RouteUrl("OrderGrid"),
                ShowUserColumn = UserItem.Role.Code == RoleCode.ADMIN,
                IsAllowedToChangeStatus = UserItem.Role.Code == RoleCode.ADMIN,
                GridItems = orders.Select(o => new OrderGridItem
                {
                    ID = o.ID,
                    Firstname = o.Firstname,
                    Lastname = o.Lastname,
                    Address = o.Address,
                    Mobile = o.Mobile,
                    StatusID = o.StatusID,
                    UserID = o.UserID,
                    TotalPrice = $"{o.OrderDetails.Sum(od => od.Price):0.00}",
                    Note = o.Note,
                    DeliveryTime = o.DeliveryTime?.ToString(Resources.FormatDate),
                    CreateTime = o.CreateTime?.ToString(Resources.FormatDate),

                    EdutUrl = Url.RouteUrl("OrdersEdit", new { ID = o.ID })
                }).ToList(),

                Users = UnitOfWork.UserRepository.GetAll().ToList().Select(u => new SimpleKeyValue<int?, string>
                {
                    Key = u.ID,
                    Value = $"{u.Firstname} {u.Lastname}"
                }).ToList(),
                Statuses = UnitOfWork.DictionaryRepository.GetAll(1, 1).Select(s => new SimpleKeyValue<int?, string>
                {
                    Key = s.ID,
                    Value = s.Caption
                }).ToList()
            };
        }

        #endregion

        #region Form

        [Route("orders/add", Name = "OrdersAdd")]
        public ActionResult OrdersAdd()
        {
            var status = UnitOfWork.DictionaryRepository.Get(1, 1, OrderStatus.PENDING);
            if (status == null)
            {
                return NotFound();
            }
            else
            {
                var order = new Order
                {
                    TotalPrice = 0,
                    UserID = UserItem.ID,
                    StatusID = status.ID
                };

                UnitOfWork.OrderRepository.Add(order);
                UnitOfWork.Complate();
                return UnitOfWork.IsError ? NotFound() : RedirectToRoute("OrdersEdit", new { ID = order.ID });
            }
        }

        [Route("orders/{ID}/edit", Name = "OrdersEdit")]
        public ActionResult OrdersEdit(int? ID)
        {
            var model = GetOrderFormViewModel(ID);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Route("orders/{ID}/edit")]
        public ActionResult OrdersEdit(OrdersFormViewModel model)
        {
            var ajaxResponse = new AjaxResponse();
            model = GetOrderFormViewModel(model.ID, model);

            UnitOfWork.OrderRepository.Update(new Order
            {
                ID = model.ID,
                UserID = model.UserID,
                StatusID = model.StatusID,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Address = model.Address,
                Mobile = model.Mobile,
                DeliveryTime = model.DeliveryTime.ToDateTime(),
                Note = model.Note
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                ajaxResponse.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    Message = Resources.Success
                };
            }

            return Json(ajaxResponse);
        }

        private OrdersFormViewModel GetOrderFormViewModel(int? orderID, OrdersFormViewModel model = null)
        {
            var order = UnitOfWork.OrderRepository.GetWithStatus(orderID);
            if (order == null)
            {
                return null;
            }
            else
            {
                if (model == null)
                {
                    model = new OrdersFormViewModel
                    {
                        Firstname = order.Firstname,
                        Lastname = order.Lastname,
                        Address = order.Address,
                        Mobile = order.Mobile,
                        DeliveryTime = order.DeliveryTime?.ToString(Resources.FormatDate),
                        Note = order.Note,
                        Status = order.Status?.Caption,
                        StatusColor = order.Status?.StringCode
                    };
                }
                model.UserID = order.UserID;
                model.StatusID = order.StatusID;
                model.SaveOrderPropertiesUrl = Url.RouteUrl("OrdersEdit", new { ID = orderID });
                model.OrdersUrl = Url.RouteUrl("Orders");
                model.OrderDetailsGridViewModel = GetOrderDetailsGridViewModel(orderID);
            }

            return model;
        }

        #endregion

        #region OrderDetails

        [Route("orders/{orderID}/order-details/grid", Name = "OrderDetailsGrid")]
        public ActionResult OrderDetailsGrid(int? orderID)
        {
            return PartialView("_OrderDetailsGrid", GetOrderDetailsGridViewModel(orderID));
        }

        [Route("orders/{orderID}/order-details/add", Name = "OrderDetailsAdd")]
        public ActionResult OrderDetailsAdd([ModelBinder(typeof(DevExpressEditorsBinder))] OrderDetailGridItem model, int? orderID)
        {
            
            UnitOfWork.OrderDetailRepository.Add(new OrderDetail
            {
                BookName = model.BookName,
                OrderID = orderID,
                Price = model.Price.ToDecimal()
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_OrderDetailsGrid", GetOrderDetailsGridViewModel(orderID));
        }

        [Route("orders/{orderID}/order-details/update", Name = "OrderDetailsUpdate")]
        public ActionResult OrderDetailsUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] OrderDetailGridItem model, int? orderID)
        {
            
            UnitOfWork.OrderDetailRepository.Update(new OrderDetail
            {
                ID = model.ID,
                BookName = model.BookName,
                OrderID = orderID,
                Price = model.Price.ToDecimal()
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_OrderDetailsGrid", GetOrderDetailsGridViewModel(orderID));
        }

        [Route("orders/{orderID}/order-details/delete", Name = "OrderDetailsDelete")]
        public ActionResult OrderDetailsDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID, int? orderID)
        {
            var orderDetail = UnitOfWork.OrderDetailRepository.Get(ID);
            UnitOfWork.OrderDetailRepository.Remove(orderDetail);
            UnitOfWork.Complate();
            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_OrderDetailsGrid", GetOrderDetailsGridViewModel(orderID));
        }

        private OrderDetailsGridViewModel GetOrderDetailsGridViewModel(int? orderID)
        {
            return new OrderDetailsGridViewModel
            {
                ListUrl = Url.RouteUrl("OrderDetailsGrid", new { orderID = orderID }),
                AddNewUrl = Url.RouteUrl("OrderDetailsAdd", new { orderID = orderID }),
                UpdateUrl = Url.RouteUrl("OrderDetailsUpdate", new { orderID = orderID }),
                DeleteUrl = Url.RouteUrl("OrderDetailsDelete", new { orderID = orderID }),
                GridItems = UnitOfWork.OrderDetailRepository.GetAll().OrderByDescending(od => od.CreateTime).Where(o => o.OrderID == orderID).ToList().Select(o => new OrderDetailGridItem
                {
                    ID = o.ID,
                    BookName = o.BookName,
                    Price = $"{o.Price:0.00}"
                }).ToList()
            };
        }

        #endregion

    }
}