using System.Web.Mvc;
using FilterAttribute = DevExpress.Utils.Filtering.FilterAttribute;

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

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}