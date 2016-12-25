using Core;
using Core.Properties;
using Core.Utilities;
using DevExpress.Web.Mvc;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class UserController : BaseController
    {
        [Route("users", Name = "Users")]
        public ActionResult Index()
        {
            var model = new UserViewModel
            {
                GridViewModel = GetGridViewModel()
            };

            return View(model);
        }

        [Route("users/grid", Name = "UsersGrid")]
        public ActionResult UserGrid()
        {
            return PartialView("_UserGrid", GetGridViewModel());
        }

        [Route("users/add", Name = "UsersAdd")]
        public ActionResult UsersAdd([ModelBinder(typeof(DevExpressEditorsBinder))] UserGridItem model)
        {
            UnitOfWork.UserRepository.Add(new User
            {
                ID = model.ID,
                Username = model.Username,
                Password = model.Password.ToMD5(),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                IsActive = model.IsActive,
                RoleID = model.RoleID
            });

            UnitOfWork.Complate();


            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UserGrid", GetGridViewModel());
        }

        [Route("users/update", Name = "UsersUpdate")]
        public ActionResult UsersUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] UserGridItem model)
        {
            var user = UnitOfWork.UserRepository.Get(model.ID);

            if (user == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                user.ID = model.ID;
                user.Username = model.Username;
                user.Password = model.Password?.ToMD5() ?? user.Password;
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.IsActive = model.IsActive;
                user.RoleID = model.RoleID;

                UnitOfWork.UserRepository.Update(user);

                UnitOfWork.Complate();

                if (UnitOfWork.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }



            return PartialView("_UserGrid", GetGridViewModel());
        }

        [Route("users/delete", Name = "UsersDelete")]
        public ActionResult UsersDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var user = UnitOfWork.UserRepository.Get(ID);
            UnitOfWork.UserRepository.Remove(user);
            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UserGrid", GetGridViewModel());
        }

        private UserGridViewModel GetGridViewModel()
        {
            var users = UnitOfWork.UserRepository.GetAll().OrderByDescending(u => u.CreateTime).ToList();
            var roles = UnitOfWork.RoleRepository.GetAll().OrderByDescending(r => r.CreateTime).ToList();

            UnitOfWork.Dispose();

            return new UserGridViewModel
            {
                ListUrl = Url.RouteUrl("UsersGrid"),
                AddNewUrl = Url.RouteUrl("UsersAdd"),
                UpdateUrl = Url.RouteUrl("UsersUpdate"),
                DeleteUrl = Url.RouteUrl("UsersDelete"),
                GridItems = users.Select(u => new UserGridItem
                {
                    ID = u.ID,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    IsActive = u.IsActive,
                    RoleID = u.RoleID
                }).ToList(),

                Roles = roles.Select(r => new SimpleKeyValue<int?, string>
                {
                    Key = r.ID,
                    Value = r.Caption
                }).ToList()
            };
        }
    }
}