using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepository
    {
        IList<Vacancy> Vacancies(int pageNo, int pageSize);
        int TotalVacancies();
        Vacancy Vacancy(int year, int month, string title);

        IList<Vacancy> VacanciesForCandidate(string workExperience, string requirements, int pageNo, int pageSize);
        int TotalVacanciesForCandidate(string workExperience, string requirements);
        Candidate Candidate(int year, int month, string title);

        IList<Vacancy> VacanciesSort(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

        void AddVacancy(Vacancy vacancy);

    }
}
