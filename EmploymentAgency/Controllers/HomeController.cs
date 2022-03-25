using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;
using System.Web.Security;
using EmploymentAgency.Models;

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
            ModelState.Remove("RoleWanted");

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

        public ViewResult ListUsers()
        {
            var viewModel = new ListUsersViewModel(_agencyRepositoryUser);

            ViewBag.Title = "Все пользователи";
            return View("ListUsers", viewModel);
        }

        public ViewResult UserProfileEdit(string username)
        {
            string current_username = System.Web.HttpContext.Current.User.Identity.Name;

            var user_return = _agencyRepositoryUser.UserForRegistration(username);
            var current_user_return = _agencyRepositoryUser.UserForRegistration(current_username);

            ViewBag.IsAdmin = current_user_return.Role.Name;

            return View(user_return);
        }

        [HttpPost]
        public ActionResult UserProfileEdit(UserAgency user)
        {
            var userReturn = _agencyRepositoryUser.UserForEdit(user.UserId);

            ModelState.Remove("RoleWanted");

            if (ModelState.IsValid)
            {
                if (userReturn == null)
                {
                    ModelState.AddModelError("", "Произошла ошибка, попробуйте позже");
                }
                else
                {
                    _agencyRepositoryUser.EditUser(user);
                    return RedirectToAction("UserProfile", "Home");
                }
            }

            return View(userReturn);
        }

        public ViewResult ApproveRoleUser(string username)
        {
            var user_return = _agencyRepositoryUser.UserForRegistration(username);
            _agencyRepositoryUser.ApproveRole(user_return);

            var viewModel = new ListUsersViewModel(_agencyRepositoryUser);

            ViewBag.Title = "Все пользователи";
            return View("ListUsers", viewModel);
        }
    }
}