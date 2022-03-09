using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;
using EmploymentAgency.Models;

namespace EmploymentAgency.Controllers
{
    [Authorize]
    public class AgencyController : Controller
    {
        private readonly IAgencyRepository _agencyRepository;

        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        public ViewResult Vacancies(int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, pageNo);

            ViewBag.Title = "Свежие вакансии";
            return View("List", viewModel);
        }

        public ViewResult Vacancy(int year, int month, string title)
        {
            var vacancy = _agencyRepository.Vacancy(year, month, title);

            if (vacancy == null)
                throw new HttpException(404, "Вакансия не найдена");

            return View(vacancy);
        }

        public ViewResult VacanciesForCandidate(string workExperience, string requirements, int year, int month, string title, int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, workExperience, requirements, year, month, title, pageNo);

            if (viewModel.Candidate == null)
                throw new HttpException(404, "Candidate not found");

            ViewBag.Title = String.Format(@"Подходящие вакансии к резюме ""{0}""",
                                viewModel.Candidate.Title);
            return View("List", viewModel);
        }

        public ViewResult VacanciesSort(string sortColumn, bool sortByAscending, int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, pageNo, sortColumn, sortByAscending);

            ViewBag.Title = "Свежие вакансии";
            return View("List", viewModel);
        }

        public ViewResult MyVacancies(int pageNo = 1)
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;

            var viewModel = new ListVacanciesViewModel(_agencyRepository, pageNo, username);

            ViewBag.Title = "Мои вакансии";
            return View("List", viewModel);
        }

        public ViewResult CreateVacancy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVacancy(Vacancy vacancy)
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;

            if (ModelState.IsValid)
            {
                _agencyRepository.AddVacancy(vacancy, username);

                return RedirectToAction("Vacancies");
            }
            return View();
        }
    }
}