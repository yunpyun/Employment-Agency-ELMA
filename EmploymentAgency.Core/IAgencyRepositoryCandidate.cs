using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepositoryCandidate
    {
        IList<Candidate> Candidates(int pageNo, int pageSize);
        int TotalCandidates();
        Candidate Candidate(int year, int month, string title);

        IList<Candidate> CandidatesForVacancy(string workExperience, string requirements, int pageNo, int pageSize);
        int TotalCandidatesForVacancy(string workExperience, string requirements);
        Vacancy Vacancy(int year, int month, string title);

    }
}
