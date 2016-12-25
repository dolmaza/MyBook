using Core;
using Core.Properties;
using Core.Utilities;
using DevExpress.Web.Mvc;
using MyBook.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class ClientController : BaseController
    {
        [Route("clients", Name = "Clients")]
        public ActionResult Index()
        {
            var model = new ClientViewModel
            {
                GridViewModel = GetGridViewModel()
            };
            return View(model);
        }

        [Route("clients/grid", Name = "ClientsGrid")]
        public ActionResult ClientGrid()
        {
            return PartialView("_ClientGrid", GetGridViewModel());
        }

        [Route("clients/add", Name = "ClientsAdd")]
        public ActionResult ClientAdd([ModelBinder(typeof(DevExpressEditorsBinder))] ClientGridItem model)
        {
            UnitOfWork.ClientRepository.Add(new Client
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Address = model.Address,
                Mobile = model.Mobile,
                UserID = UserItem.ID,
                StatusID = model.StatusID
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_ClientGrid", GetGridViewModel());
        }

        [Route("clients/update", Name = "ClientsUpdate")]
        public ActionResult ClientUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ClientGridItem model)
        {
            UnitOfWork.ClientRepository.Update(new Client
            {
                ID = model.ID,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Address = model.Address,
                Mobile = model.Mobile,
                UserID = UserItem.ID,
                StatusID = model.StatusID
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_ClientGrid", GetGridViewModel());
        }

        [Route("clients/delete", Name = "ClientsDelete")]
        public ActionResult ClientDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var client = UnitOfWork.ClientRepository.Get(ID);
            UnitOfWork.ClientRepository.Remove(client);
            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_ClientGrid", GetGridViewModel());
        }

        private ClientGridViewModel GetGridViewModel()
        {
            var clients = UserItem.Role.Code == RoleCode.ADMIN ? UnitOfWork.ClientRepository.GetAll().OrderByDescending(c => c.CreateTime).ToList() :
                UnitOfWork.ClientRepository.GetAll().Where(c => c.UserID == UserItem.ID).OrderByDescending(c => c.CreateTime).ToList();
            var statuses = UnitOfWork.DictionaryRepository.GetAll(1, 2).OrderBy(c => c.SortIndex).ToList();

            UnitOfWork.Dispose();

            return new ClientGridViewModel
            {
                ListUrl = Url.RouteUrl("ClientsGrid"),
                AddNewUrl = Url.RouteUrl("ClientsAdd"),
                UpdateUrl = Url.RouteUrl("ClientsUpdate"),
                DeleteUrl = Url.RouteUrl("ClientsDelete"),
                CreateOrderUrl = Url.RouteUrl("OrdersAdd"),

                GridItems = clients.Select(c => new ClientGridItem
                {
                    ID = c.ID,
                    UserID = c.UserID,
                    StatusID = c.StatusID,
                    Firstname = c.Firstname,
                    Lastname = c.Lastname,
                    Address = c.Address,
                    Mobile = c.Mobile
                }).ToList(),

                Statuses = statuses.Select(s => new SimpleKeyValue<int?, string>
                {
                    Key = s.ID,
                    Value = s.Caption
                }).ToList()
            };
        }
    }
}