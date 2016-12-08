using Core;
using Core.Properties;
using DevExpress.Web.Mvc;
using MyBook.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class RoleController : BaseController
    {
        [Route("roles", Name = "Roles")]
        public ActionResult Index()
        {
            var model = new RoleViewModel
            {
                GridViewModel = GetGridViewModel()
            };

            return View(model);
        }

        [Route("roles/grid", Name = "RolesGrid")]
        public ActionResult RoleGrid()
        {
            return PartialView("_RoleGrid", GetGridViewModel());
        }

        [Route("roles/add", Name = "RolesAdd")]
        public ActionResult RolesAdd([ModelBinder(typeof(DevExpressEditorsBinder))] RoleGridItem model)
        {
            UnitOfWork.RoleRepository.Add(new Role
            {
                ID = model.ID,
                Caption = model.Caption,
                Code = model.Code
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_RoleGrid", GetGridViewModel());
        }

        [Route("roles/update", Name = "RolesUpdate")]
        public ActionResult RolesUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] RoleGridItem model)
        {
            UnitOfWork.RoleRepository.Update(new Role
            {
                ID = model.ID,
                Caption = model.Caption,
                Code = model.Code
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_RoleGrid", GetGridViewModel());
        }

        [Route("roles/delete", Name = "RolesDelete")]
        public ActionResult RolesDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var role = UnitOfWork.RoleRepository.Get(ID);
            UnitOfWork.RoleRepository.Remove(role);

            UnitOfWork.Complate();
            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_RoleGrid", GetGridViewModel());
        }

        private RoleGridViewModel GetGridViewModel()
        {
            return new RoleGridViewModel
            {
                ListUrl = Url.RouteUrl("RolesGrid"),
                AddNewUrl = Url.RouteUrl("RolesAdd"),
                UpdateUrl = Url.RouteUrl("RolesUpdate"),
                DeleteUrl = Url.RouteUrl("RolesDelete"),
                GridItems = UnitOfWork.RoleRepository.GetAll().Select(r => new RoleGridItem
                {
                    ID = r.ID,
                    Caption = r.Caption,
                    Code = r.Code
                }).ToList()
            };
        }
    }
}