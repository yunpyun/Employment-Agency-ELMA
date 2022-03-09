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
        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, int pageNo)
        {
            Vacancies = _agencyRepository.Vacancies(pageNo - 1, 10);
            TotalVacancies = _agencyRepository.TotalVacancies();
        }

        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, int pageNo, string sortColumn, bool sortByAscending)
        {
            Vacancies = _agencyRepository.VacanciesSort(pageNo - 1, 10, sortColumn, sortByAscending);
            TotalVacancies = _agencyRepository.TotalVacancies();
        }

        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, string workExperience, string requirements, 
            int year, int month, string title, int p)
        {
                Vacancies = _agencyRepository.VacanciesForCandidate(workExperience, requirements, p - 1, 10);
                TotalVacancies = _agencyRepository.TotalVacanciesForCandidate(workExperience, requirements);
                Candidate = _agencyRepository.Candidate(year, month, title);
        }

        public ListVacanciesViewModel(IAgencyRepository _agencyRepository, int pageNo, string username)
        {
            Vacancies = _agencyRepository.MyVacancies(pageNo - 1, 10, username);
            TotalVacancies = _agencyRepository.TotalMyVacancies(username);
        }

        public IList<Vacancy> Vacancies { get; private set; }
        public int TotalVacancies { get; private set; }

        public Candidate Candidate { get; private set; }
    }
}