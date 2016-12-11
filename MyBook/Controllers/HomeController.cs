using Core.Properties;
using Core.Utilities;
using MyBook.Models;
using SmartExpress.Reusable.Extentions;
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

        [HttpGet]
        [Route("login", Name = "Login")]
        public ActionResult Login()
        {
            var model = new LoginViewModel
            {
                LoginUrl = Url.RouteUrl("Login"),
                RedirectUrl = Request.QueryString["RedirectUrl"]
            };

            return View(model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = UnitOfWork.UserRepository.Get(model.Username, model.Password?.ToMD5());

            if (user == null)
            {
                model.ErrorMessage = Resources.ValidationInvalidUsernameOrPassword;
                model.Password = null;
            }
            else if (!user.IsActive)
            {
                model.ErrorMessage = Resources.ValidationInvalidUsernameOrPassword;
                model.Password = null;
            }
            else
            {
                Session[AppSettings.AuthenticatedUserKey] = user;
                return Redirect(model.RedirectUrl ?? "/");
            }

            return View(model);
        }

        [Route("logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToRoute("Dashboard");
        }
    }
}