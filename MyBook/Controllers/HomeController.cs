using MyBook.Models;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class HomeController : BaseController
    {
        [Route("", Name = "Dashboard")]
        public ActionResult Index()
        {
            return View();
        }
    }
}