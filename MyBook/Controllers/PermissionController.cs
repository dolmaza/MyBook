using Core;
using Core.Properties;
using DevExpress.Web.Mvc;
using MyBook.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class PermissionController : BaseController
    {
        [Route("permissions", Name = "Permissions")]
        public ActionResult Index()
        {
            var model = new PermissionViewModel
            {
                TreeViewModel = GetTreeViewModel()
            };

            return View(model);
        }

        [Route("permissions/tree", Name = "PermissionsTree")]
        public ActionResult PermissionTree()
        {
            return PartialView("_PermissionTree", GetTreeViewModel());
        }

        [Route("permissions/add", Name = "PermissionsAdd")]
        public ActionResult PemissionsAdd([ModelBinder(typeof(DevExpressEditorsBinder))] PermissionTreeItem model)
        {
            UnitOfWork.PermissionRepository.Add(new Permission
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                Url = model.Url,
                Code = model.Code ?? Guid.NewGuid().ToString(),
                IsMenuItem = model.IsMenuItem,
                IconName = model.IconName,
                SortIndex = model.SortIndex
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_PermissionTree", GetTreeViewModel());
        }

        [Route("permissions/update", Name = "PermissionsUpdate")]
        public ActionResult PemissionsUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] PermissionTreeItem model)
        {
            UnitOfWork.PermissionRepository.Update(new Permission
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                Url = model.Url,
                Code = model.Code ?? Guid.NewGuid().ToString(),
                IsMenuItem = model.IsMenuItem,
                IconName = model.IconName,
                SortIndex = model.SortIndex
            });

            UnitOfWork.Complate();


            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_PermissionTree", GetTreeViewModel());
        }

        [Route("permissions/delete", Name = "PermissionsDelete")]
        public ActionResult PermissionsDelete(int? ID)
        {
            var permission = UnitOfWork.PermissionRepository.Get(ID);
            UnitOfWork.PermissionRepository.Remove(permission);

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_PermissionTree", GetTreeViewModel());
        }

        private PermissionTreeViewModel GetTreeViewModel()
        {
            return new PermissionTreeViewModel
            {
                ListUrl = Url.RouteUrl("PermissionsTree"),
                AddNewUrl = Url.RouteUrl("PermissionsAdd"),
                UpdateUrl = Url.RouteUrl("PermissionsUpdate"),
                DeleteUrl = Url.RouteUrl("PermissionsDelete"),
                TreeItems = UnitOfWork.PermissionRepository.GetAll().Select(p => new PermissionTreeItem
                {
                    ID = p.ID,
                    ParentID = p.ParentID,
                    Caption = p.Caption,
                    Url = p.Url,
                    Code = p.Code,
                    IconName = p.IconName,
                    IsMenuItem = p.IsMenuItem,
                    SortIndex = p.SortIndex
                }).ToList()
            };
        }
    }
}