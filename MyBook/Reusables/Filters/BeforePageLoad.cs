using Core;
using Core.Utilities;
using System.Web.Mvc;

namespace MyBook.Reusables.Filters
{
    public class BeforePageLoad : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserAutorize(filterContext);
        }

        private void UserAutorize(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData != null && (filterContext.RouteData.Values["action"].ToString() != "Login"))
            {
                var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;

                if (user == null)
                {
                    filterContext.Result = new RedirectResult("/login");
                }
                else
                {

                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}