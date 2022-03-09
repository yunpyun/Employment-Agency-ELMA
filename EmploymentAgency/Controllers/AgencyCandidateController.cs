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
    public class AgencyCandidateController : Controller
    {
        private readonly IAgencyRepositoryCandidate _agencyRepositoryCandidate;

        public AgencyCandidateController(IAgencyRepositoryCandidate agencyRepositoryCandidate)
        {
            _agencyRepositoryCandidate = agencyRepositoryCandidate;
        }

        public ViewResult Candidates(int pageNo = 1)
        {
            var viewModel = new ListCandidatesViewModel(_agencyRepositoryCandidate, pageNo);

            ViewBag.Title = "Свежие резюме";
            return View("ListCandidates", viewModel);
        }

        public ViewResult Candidate(int year, int month, string title)
        {
            var candidate = _agencyRepositoryCandidate.Candidate(year, month, title);

            if (candidate == null)
                throw new HttpException(404, "Вакансия не найдена");

            return View(candidate);
        }

        public ViewResult CandidatesForVacancy(string workExperience, string requirements, int year, int month, string title, int pageNo = 1)
        {
            var viewModel = new ListCandidatesViewModel(_agencyRepositoryCandidate, workExperience, requirements, year, month, title, pageNo);

            if (viewModel.Vacancy == null)
                throw new HttpException(404, "Vacancy not found");

            ViewBag.Title = String.Format(@"Подходящие резюме к вакансии ""{0}""",
                                viewModel.Vacancy.Name);
            return View("ListCandidates", viewModel);
        }

        public ViewResult MyCandidates(int pageNo = 1)
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;

            var viewModel = new ListCandidatesViewModel(_agencyRepositoryCandidate, pageNo, username);

            ViewBag.Title = "Мои резюме";
            return View("ListCandidates", viewModel);
        }
    }
}