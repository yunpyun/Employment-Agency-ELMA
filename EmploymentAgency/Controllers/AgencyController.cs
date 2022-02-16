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
    public class AgencyController : Controller
    {
        // GET: Agency
        /*public ActionResult Index()
        {
            return View();
        }*/

        private readonly IAgencyRepository _agencyRepository;

        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        public ViewResult Vacancies(int p = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, p);

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

        public ViewResult Candidates(int p = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, p);

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
            /*var id = _agencyRepository.AddVacancy(vacancy);*/

            if (ModelState.IsValid)
            {
                _agencyRepository.AddVacancyMSSQL(vacancy);

                return RedirectToAction("Vacancies");
            }
            return View();
        }

        public ViewResult CandidatesForVacancy(string workExperience, string requirements, int year, int month, string title, int p = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, workExperience, requirements, year, month, title, p, "Резюме");

            if (viewModel.Vacancy == null)
                throw new HttpException(404, "Vacancy not found");

            ViewBag.Title = String.Format(@"Подходящие резюме к вакансии ""{0}""",
                                viewModel.Vacancy.Name);
            return View("ListCandidates", viewModel);
        }

        public ViewResult VacanciesForCandidate(string workExperience, string requirements, int year, int month, string title, int p = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, workExperience, requirements, year, month, title, p, "Вакансии");

            if (viewModel.Candidate == null)
                throw new HttpException(404, "Candidate not found");

            ViewBag.Title = String.Format(@"Подходящие вакансии к резюме ""{0}""",
                                viewModel.Candidate.Title);
            return View("List", viewModel);
        }

        public ViewResult VacanciesSort(string sortColumn, bool sortByAscending, int p = 1)
        {
            var viewModel = new ListVacanciesViewModel(_agencyRepository, p, sortColumn, sortByAscending);

            ViewBag.Title = "Свежие вакансии";
            return View("List", viewModel);
        }

    }
}