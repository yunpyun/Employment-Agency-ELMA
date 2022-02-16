using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Models
{
    public class ListVacanciesViewModel
    {
        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, int p)
        {
            Vacancies = _agencyRepository.Vacancies(p - 1, 10);
            TotalVacancies = _agencyRepository.TotalVacancies();

            Candidates = _agencyRepository.Candidates(p - 1, 10);
            TotalCandidates = _agencyRepository.TotalCandidates();
        }

        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, int p, string sortColumn, bool sortByAscending)
        {
            Vacancies = _agencyRepository.Vacancies(p - 1, 10, sortColumn, sortByAscending);
            TotalVacancies = _agencyRepository.TotalVacancies();
        }

        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, string workExperience, string requirements, 
            int year, int month, string title, int p, string typeObject)
        {
            if (typeObject == "Резюме")
            {
                Candidates = _agencyRepository.CandidatesForVacancy(workExperience, requirements, p - 1, 10);
                TotalCandidates = _agencyRepository.TotalCandidatesForVacancy(workExperience, requirements);
                Vacancy = _agencyRepository.Vacancy(year, month, title);
            }
            if (typeObject == "Вакансии")
            {
                Vacancies = _agencyRepository.VacanciesForCandidate(workExperience, requirements, p - 1, 10);
                TotalVacancies = _agencyRepository.TotalVacanciesForCandidate(workExperience, requirements);
                Candidate = _agencyRepository.Candidate(year, month, title);
            }
        }

        public IList<Vacancy> Vacancies { get; private set; }
        public int TotalVacancies { get; private set; }

        public IList<Candidate> Candidates { get; private set; }
        public int TotalCandidates { get; private set; }

        public Vacancy Vacancy { get; private set; }

        public Candidate Candidate { get; private set; }
    }
}