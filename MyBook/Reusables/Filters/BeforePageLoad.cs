using Core;
using Core.Utilities;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
using System.Web.Mvc;

namespace MyBook.Reusables.Filters
{
    public class BeforePageLoad : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (BaseController)filterContext.Controller;
            GetUserFromSession(filterContext, ref controller);
            UserAutorize(filterContext);
        }

        private void UserAutorize(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData != null && (filterContext.RouteData.Values["action"].ToString() != "Login"))
            {
                var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
                var requestedUrl = filterContext.HttpContext.Request.RawUrl;
                var redirectUrl = filterContext.HttpContext.Request.Url?.AbsolutePath;
                if (user == null)
                {
                    filterContext.Result = new RedirectResult($"/login?RedirectUrl={redirectUrl}");
                }
                else if (!user.HasUserPermission(requestedUrl))
                {
                    filterContext.Result = new RedirectResult($"/login?RedirectUrl={redirectUrl}");
                }

            }
        }

        private void GetUserFromSession(ActionExecutingContext filterContext, ref BaseController contorller)
        {
            var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
            contorller.UserItem = user;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}