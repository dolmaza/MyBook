using Core;
using Core.Properties;
using Core.Utilities;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
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
            return new OrderGridViewModel
            {
                ListUrl = Url.RouteUrl("OrderGrid"),
                GridItems = UnitOfWork.OrderRepository.GetAll().ToList().Select(o => new OrderGridItem
                {
                    ID = o.ID,
                    Firstname = o.Firstname,
                    Lastname = o.Lastname,
                    Address = o.Address,
                    Mobile = o.Mobile,
                    StatusID = o.StatusID,
                    UserID = o.UserID,
                    TotalPrice = o.TotalPrice,
                    Note = o.Note,
                    DeliveryTime = o.DeliveryTime,
                    CreateTime = o.CreateTime,

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

        private OrderDetailsGridViewModel GetOrderDetailsGridViewModel(int? orderID)
        {
            return new OrderDetailsGridViewModel
            {
                ListUrl = Url.RouteUrl("OrderDetailsGrid", new { orderID = orderID }),
                GridItems = UnitOfWork.OrderDetailRepository.GetAll().Where(o => o.OrderID == orderID).ToList().Select(o => new OrderDetailGridItem
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