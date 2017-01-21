using Core;
using Core.Properties;
using Core.Utilities;
using Core.Validation;
using DevExpress.Web.Mvc;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
using System;
using System.Collections.Generic;
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
                IsAllowedToArchiveOrders = UserItem.Role.Code == RoleCode.ADMIN,
                ArchiveOrdersUrl = Url.RouteUrl("OrdersArchive")
            };

            return View(model);
        }

        [Route("orders/grid", Name = "OrderGrid")]
        public ActionResult OrderGrid()
        {
            return PartialView("_OrderGrid", GetOrderGridViewModel());
        }

        [Route("orders/archive", Name = "OrdersArchive")]
        public ActionResult OrdersArchive(List<int?> orderIDs)
        {
            var ajaxResponse = new AjaxResponse();

            if (orderIDs == null || orderIDs.Count == 0)
            {
                ajaxResponse.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                UnitOfWork.OrderRepository.OrdersArchive(orderIDs);
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

            }

            return Json(ajaxResponse);
        }

        [Route("orders/delete", Name = "OrdersDelete")]
        public ActionResult OrderDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var order = UnitOfWork.OrderRepository.Get(ID);
            UnitOfWork.OrderRepository.Remove(order);
            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_OrderGrid", GetOrderGridViewModel());
        }

        private OrderGridViewModel GetOrderGridViewModel()
        {
            var orders = UserItem.Role.Code == RoleCode.ADMIN ?
                UnitOfWork.OrderRepository.GetAll()
                .Include(o => o.OrderDetails)
                .Where(o => !o.IsArchived)
                .OrderByDescending(o => o.CreateTime)
                .AsNoTracking()
                .ToList() :
                UnitOfWork.OrderRepository.GetAll()
                .Include(o => o.OrderDetails)
                .Include(o => o.Status)
                .Where(o => o.UserID == UserItem.ID && !o.IsArchived)
                .OrderByDescending(o => o.CreateTime)
                .AsNoTracking()
                .ToList();


            return new OrderGridViewModel
            {
                ListUrl = Url.RouteUrl("OrderGrid"),
                DeleteUrl = Url.RouteUrl("OrdersDelete"),
                ShowUserColumn = UserItem.Role.Code == RoleCode.ADMIN,
                IsAllowedToChangeStatus = UserItem.Role.Code == RoleCode.ADMIN,
                IsAllowedToDeleteOrder = UserItem.HasUserPermission(Url.RouteUrl("OrdersDelete")),
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

                    IsAllowedToEditOrder = (UserItem.Role.Code == RoleCode.ADMIN) || UserItem.Role.Code != RoleCode.ADMIN && o.Status.IntCode == OrderStatus.PENDING,

                    EdutUrl = Url.RouteUrl("OrdersEdit", new { ID = o.ID }),
                    PaperUrl = Url.RouteUrl("OrderPaper", new { orderID = o.ID }),
                    StatusUpdateUrl = Url.RouteUrl("OrderStatusUpdate", new { orderID = o.ID })
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

        [Route("orders/archived", Name = "OrdersArchvied")]
        public ActionResult OrdersArchived()
        {
            var model = new OrderArchivedViewModel
            {
                GridViewModel = GetOrderArchivedGridViewModel(),
                IsAllowedToUnArchiveOrders = UserItem.Role.Code == RoleCode.ADMIN,
                UnArchiveOrdersUrl = Url.RouteUrl("OrdersUnArchive")
            };

            return View(model);
        }

        [Route("orders/archived/grid", Name = "OrdersArchivedGrid")]
        public ActionResult OrdersArchivedGrid()
        {
            return PartialView("_OrderArchivedGrid", GetOrderArchivedGridViewModel());
        }

        [Route("orders/unarchive", Name = "OrdersUnArchive")]
        public ActionResult OrderUnArchive(List<int?> orderIDs)
        {
            var ajaxResponse = new AjaxResponse();

            if (orderIDs == null || orderIDs.Count == 0)
            {
                ajaxResponse.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                UnitOfWork.OrderRepository.OrdersUnArchive(orderIDs);
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

            }

            return Json(ajaxResponse);
        }


        private OrderArchivedGridViewModel GetOrderArchivedGridViewModel()
        {
            var orders = UserItem.Role.Code == RoleCode.ADMIN ?
                UnitOfWork.OrderRepository.GetAll()
                .Include(o => o.OrderDetails)
                .Where(o => o.IsArchived)
                .OrderByDescending(o => o.CreateTime)
                .AsNoTracking()
                .ToList() :
                UnitOfWork.OrderRepository.GetAll()
                .Include(o => o.OrderDetails)
                .Include(o => o.Status)
                .Where(o => o.UserID == UserItem.ID && o.IsArchived)
                .OrderByDescending(o => o.CreateTime)
                .AsNoTracking()
                .ToList();


            return new OrderArchivedGridViewModel
            {
                ListUrl = Url.RouteUrl("OrdersArchivedGrid"),
                ShowUserColumn = UserItem.Role.Code == RoleCode.ADMIN,
                IsAllowedToDeleteOrder = UserItem.HasUserPermission(Url.RouteUrl("OrdersDelete")),
                GridItems = orders.Select(o => new OrderArchivedGridItem
                {
                    ID = o.ID,
                    Firstname = o.Firstname,
                    Lastname = o.Lastname,
                    Address = o.Address,
                    Mobile = o.Mobile,
                    UserID = o.UserID,
                    TotalPrice = $"{o.OrderDetails.Sum(od => od.Price):0.00}",
                    Note = o.Note,
                    DeliveryTime = o.DeliveryTime?.ToString(Resources.FormatDate),
                    CreateTime = o.CreateTime?.ToString(Resources.FormatDate),
                    PaperUrl = Url.RouteUrl("OrderPaper", new { orderID = o.ID }),
                }).ToList(),

                Users = UnitOfWork.UserRepository.GetAll().ToList().Select(u => new SimpleKeyValue<int?, string>
                {
                    Key = u.ID,
                    Value = $"{u.Firstname} {u.Lastname}"
                }).ToList()
            };
        }

        #endregion

        #region Form

        [HttpPost]
        [Route("orders/add", Name = "OrdersAdd")]
        public ActionResult OrdersAdd(int? clientID)
        {
            var ajaxResponse = new AjaxResponse();
            var client = UnitOfWork.ClientRepository.Get(clientID);

            if (client == null)
            {
                ajaxResponse.Data = new
                {
                    RedirectUrl = Url.RouteUrl("NotFound")
                };
            }
            else
            {
                var status = UnitOfWork.DictionaryRepository.Get(1, 1, OrderStatus.PENDING);
                if (status == null)
                {
                    ajaxResponse.Data = new
                    {
                        RedirectUrl = Url.RouteUrl("NotFound")
                    };
                }
                else
                {
                    var order = new Order
                    {
                        TotalPrice = 0,
                        UserID = UserItem.ID,
                        StatusID = status.ID,
                        Firstname = client.Firstname,
                        Lastname = client.Lastname,
                        Address = client.Address,
                        Mobile = client.Mobile,
                        ClientID = client.ID
                    };

                    UnitOfWork.OrderRepository.Add(order);
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
                            RedirectUrl = Url.RouteUrl("OrdersEdit", new { ID = order.ID })
                        };
                    }
                }
            }
            return Json(ajaxResponse);

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
            var errors = Validation.ValidateOrderEditForm(model.Firstname, model.Lastname, model.Address, model.Mobile, model.DeliveryTime);
            if (errors.Count == 0)
            {
                model = GetOrderFormViewModel(model.ID, model);

                if (model == null)
                {
                    ajaxResponse.Data = new
                    {
                        RedirectUrl = Url.RouteUrl("NotFound")
                    };
                }
                else
                {

                    UnitOfWork.OrderRepository.Update(new Order
                    {
                        ID = model.ID,
                        UserID = model.UserID,
                        StatusID = model.StatusID,
                        ClientID = model.ClientID,
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
                }
            }
            else
            {
                ajaxResponse.Data = new
                {
                    ErrorsJson = errors.ToJson()
                };
            }

            return Json(ajaxResponse);
        }

        private OrdersFormViewModel GetOrderFormViewModel(int? orderID, OrdersFormViewModel model = null)
        {
            var order = UnitOfWork.OrderRepository.GetWithStatus(orderID);
            if (order == null || (UserItem.Role.Code == RoleCode.BROCKER && order.Status.IntCode != OrderStatus.PENDING))
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
                model.ClientID = order.ClientID;
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

        [Route("orders/sold-books", Name = "SoldBooks")]
        public ActionResult SoldBooks()
        {
            var model = new SoldBooksViewModel
            {
                GridViewModel = GetSoldBooksGridViewModel()
            };

            return View(model);
        }

        [Route("orders/sold-books/grid", Name = "SoldBooksGrid")]
        public ActionResult SoldBooksGrid()
        {
            return PartialView("_SoldBooksGrid", GetSoldBooksGridViewModel());
        }

        private SoldBooksGridViewModel GetSoldBooksGridViewModel()
        {
            var soldBooks = UserItem.Role.Code == RoleCode.ADMIN
                ? UnitOfWork.OrderDetailRepository.GetAll()
                    .Include(od => od.Order)
                    .Include(od => od.Order.Status)
                    .Include(od => od.Order.Client)
                    .Where(od => od.Order.Status.IntCode == OrderStatus.COMPLATED)
                    .OrderByDescending(od => od.CreateTime)
                    .ToList() :
                    UnitOfWork.OrderDetailRepository.GetAll()
                    .Include(od => od.Order)
                    .Include(od => od.Order.Client)
                    .Include(od => od.Order.Status)
                    .Where(od => od.Order.UserID == UserItem.ID && od.Order.Status.IntCode == OrderStatus.COMPLATED)
                    .OrderByDescending(od => od.CreateTime)
                    .ToList();

            var clients = UserItem.Role.Code == RoleCode.ADMIN
                ? UnitOfWork.ClientRepository.GetAll().ToList()
                : UnitOfWork.ClientRepository.GetAll().Where(c => c.UserID == UserItem.ID).ToList();

            return new SoldBooksGridViewModel
            {
                ListUrl = Url.RouteUrl("SoldBooksGrid"),
                CreateOrderUrl = Url.RouteUrl("OrdersAdd"),

                GridItems = soldBooks.Select(s => new SoldBookGridItem
                {
                    ID = s.ID,
                    ClientID = s.Order.ClientID,
                    Mobile = s.Order.Client.Mobile,
                    BookName = s.BookName,
                    Price = $"{s.Price:0.00}"
                }).ToList(),

                Clients = clients.Select(c => new SimpleKeyValue<int?, string>
                {
                    Key = c.ID,
                    Value = $"{c.Firstname} {c.Lastname}"
                }).ToList()
            };
        }

        #endregion

        #region Popup

        [Route("orders/{orderID}/status-update", Name = "OrderStatusUpdate")]
        public ActionResult OrderStatusUpdate(int? orderID)
        {
            var order = UnitOfWork.OrderRepository.Get(orderID);
            var model = new OrderStatusUpdateViewModel
            {
                Statuses = UnitOfWork.DictionaryRepository.GetAll(1, 1).Select(s => new SimpleKeyValue<int?, string>
                {
                    Key = s.ID,
                    Value = s.Caption,
                    IsSelected = order.StatusID == s.ID
                }).ToList(),

                StatusSaveUrl = Url.RouteUrl("OrderStatusUpdate", new { orderID = orderID })
            };

            return View(model);
        }

        [HttpPost]
        [Route("orders/{orderID}/status-update")]
        public ActionResult OrderStatusUpdate(int? orderID, OrderStatusUpdateViewModel model)
        {
            var ajaxResponse = new AjaxResponse();

            var order = UnitOfWork.OrderRepository.Get(orderID);

            order.StatusID = model.StatusID;

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
            }

            return Json(ajaxResponse);
        }

        [Route("orders/{orderID}/paper", Name = "OrderPaper")]
        public ActionResult OrderPaper(int? orderID)
        {
            var order = UnitOfWork.OrderRepository.GetOrderPaper(orderID);
            UnitOfWork.Dispose();

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                var model = new OrderPaperViewModel
                {
                    Firstname = order.Firstname,
                    Lastname = order.Lastname,
                    Address = order.Address,
                    Mobile = order.Mobile,
                    DeliveryTime = order.DeliveryTime?.ToString(Resources.FormatDate),
                    Note = order.Note,
                    Status = order.Status.Caption,

                    Grid = GetOrderPaperGridViewModel(order)
                };

                return View(model);
            }

        }

        [Route("orders/{orderID}/paper/grid", Name = "OrderPaperGird")]
        public ActionResult OrderPaperGrid(int? orderID)
        {
            var order = UnitOfWork.OrderRepository.GetOrderPaper(orderID);
            UnitOfWork.Dispose();

            return PartialView("_OrderPaperGrid", GetOrderPaperGridViewModel(order));
        }

        private OrderPaperViewModel.GridViewModel GetOrderPaperGridViewModel(Order order)
        {
            return new OrderPaperViewModel.GridViewModel
            {
                ListUrl = Url.RouteUrl("OrderPaperGird", new { orderID = order.ID }),
                GridItems = order.OrderDetails.Select(od => new OrderPaperViewModel.GridViewModel.GridItem
                {
                    ID = od.ID,
                    BookName = od.BookName,
                    Price = $"{od.Price:0.00}"
                }).ToList()
            };
        }

        #endregion

    }
}