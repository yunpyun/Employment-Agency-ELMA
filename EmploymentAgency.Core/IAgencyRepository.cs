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

        IList<Candidate> Candidates(int pageNo, int pageSize);
        int TotalCandidates();
        Candidate Candidate(int year, int month, string title);

        void AddVacancy(Vacancy vacancy);

        IList<Candidate> CandidatesForVacancy(string workExperience, string requirements, int pageNo, int pageSize);
        int TotalCandidatesForVacancy(string workExperience, string requirements);

        IList<Vacancy> VacanciesForCandidate(string workExperience, string requirements, int pageNo, int pageSize);
        int TotalVacanciesForCandidate(string workExperience, string requirements);

        IList<Vacancy> Vacancies(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

        User User(string login, string pwd);
    }
}
