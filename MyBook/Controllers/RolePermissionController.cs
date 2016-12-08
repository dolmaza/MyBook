using Core;
using Core.Properties;
using Core.Utilities;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class RolePermissionController : BaseController
    {
        [Route("role-permissions", Name = "RolePermissions")]
        public ActionResult Index()
        {
            var model = new RolePermissionsViewModel
            {
                RolesGridViewModel = GetRolesGridViewModel(),
                PermissionsTreeViewModel = GetPermissionsTreeViewModel(),

                GetRolePermissionsUrl = Url.RouteUrl("GetRolePermissions"),
                UpdateRolePermissionsUrl = Url.RouteUrl("RolePermissionsUpdate")
            };

            return View(model);
        }

        [Route("role-permissions/roles/grid", Name = "RolePermissionsRolesGrid")]
        public ActionResult RolesGrid()
        {
            return PartialView("_RolesGrid", GetRolesGridViewModel());
        }

        [Route("role-permissions/permissions/tree", Name = "RolePermissionsPermissionsTree")]
        public ActionResult PermissionsTree()
        {
            return PartialView("_PermissionsTree", GetPermissionsTreeViewModel());
        }

        [Route("role-permissions/get-role-permissions", Name = "GetRolePermissions")]
        public ActionResult GetRolePermissions(int? ID)
        {
            var ajaxResponse = new AjaxResponse();

            var permissions = UnitOfWork.RoleRepository.GetRolePermissions(ID);

            ajaxResponse.IsSuccess = true;
            ajaxResponse.Data = new
            {
                Permissions = permissions.ToJson()
            };

            return Json(ajaxResponse);
        }

        [Route("role-permissions/update", Name = "RolePermissionsUpdate")]
        public ActionResult RolePermissionsUpdate(int? roleID, List<int?> permissions)
        {
            var ajaxResponse = new AjaxResponse();

            var role = UnitOfWork.RoleRepository.Get(roleID);

            if (role == null)
            {
                ajaxResponse.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                var newPermissions = new List<Permission>();
                permissions?.ForEach(id =>
                {
                    newPermissions.Add(UnitOfWork.PermissionRepository.Get(id));
                });

                UnitOfWork.RoleRepository.UpdateRolePermissions(roleID, newPermissions);
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

        private RolePermissionsRolesGridViewModel GetRolesGridViewModel()
        {
            return new RolePermissionsRolesGridViewModel
            {
                ListUrl = Url.RouteUrl("RolePermissionsRolesGrid"),
                GridItems = UnitOfWork.RoleRepository.GetAll().Select(r => new RolePermissionsRoleGridItem
                {
                    ID = r.ID,
                    Caption = r.Caption
                }).ToList()
            };
        }

        private RolePermissionsPermissionsTreeViewModel GetPermissionsTreeViewModel()
        {
            return new RolePermissionsPermissionsTreeViewModel
            {
                ListUrl = Url.RouteUrl("RolePermissionsPermissionsTree"),
                TreeItems = UnitOfWork.PermissionRepository.GetAll().Select(p => new RolePermissionsPermissionTreeItem
                {
                    ID = p.ID,
                    ParentID = p.ParentID,
                    Caption = p.Caption
                }).ToList()
            };
        }
    }
}