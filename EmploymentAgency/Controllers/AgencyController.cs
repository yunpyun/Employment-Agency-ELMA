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

        public ViewResult Candidates(int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, pageNo);

            ViewBag.Title = "Свежие резюме";
            return View("ListCandidates", viewModel);
        }

        public ViewResult Candidate(int year, int month, string title)
        {
            var candidate = _agencyRepository.Candidate(year, month, title);

            if (candidate == null)
                throw new HttpException(404, "Вакансия не найдена");

            return View(candidate);
        }

        public ViewResult CreateVacancy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVacancy(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                _agencyRepository.AddVacancy(vacancy);

                return RedirectToAction("Vacancies");
            }
            return View();
        }

        public ViewResult CandidatesForVacancy(string workExperience, string requirements, int year, int month, string title, int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, workExperience, requirements, year, month, title, pageNo, "Резюме");

            if (viewModel.Vacancy == null)
                throw new HttpException(404, "Vacancy not found");

            ViewBag.Title = String.Format(@"Подходящие резюме к вакансии ""{0}""",
                                viewModel.Vacancy.Name);
            return View("ListCandidates", viewModel);
        }

        public ViewResult VacanciesForCandidate(string workExperience, string requirements, int year, int month, string title, int pageNo = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, workExperience, requirements, year, month, title, pageNo, "Вакансии");

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

    }
}