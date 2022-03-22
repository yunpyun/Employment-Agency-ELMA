using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;
using System.Web.Security;

namespace EmploymentAgency.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAgencyRepositoryUser _agencyRepositoryUser;

        public HomeController(IAgencyRepositoryUser agencyRepositoryUser)
        {
            _agencyRepositoryUser = agencyRepositoryUser;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAgency user)
        {
            var user_return = _agencyRepositoryUser.User(user.Email, user.Password);

            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");

            if (ModelState.IsValid)
            {
                if (user_return != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Vacancies", "Agency");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(user_return);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View("Registration");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserAgency user)
        {
            var user_return = _agencyRepositoryUser.UserForRegistration(user.Email);
            if (ModelState.IsValid)
            {
                if (user_return != null)
                {
                    ModelState.AddModelError("", "Уже есть пользователь с таким email");
                }
                else
                {
                    _agencyRepositoryUser.AddUser(user);
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Vacancies", "Agency");
                }
            }

            return View(user_return);
        }

        public ViewResult UserProfile()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;

            var user_return = _agencyRepositoryUser.UserForRegistration(username);

            return View(user_return);
        }
    }
}