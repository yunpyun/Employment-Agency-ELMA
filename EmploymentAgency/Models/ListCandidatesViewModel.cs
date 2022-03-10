using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Models
{
    public class ListCandidatesViewModel
    {
        public ListCandidatesViewModel(IAgencyRepositoryCandidate _agencyRepositoryCandidate, int pageNo)
        {
            Candidates = _agencyRepositoryCandidate.Candidates(pageNo - 1, 10);
            TotalCandidates = _agencyRepositoryCandidate.TotalCandidates();
        }

        public ListCandidatesViewModel(IAgencyRepositoryCandidate _agencyRepositoryCandidate, int vacancyId,
            int year, int month, string title, int p)
        {
            Candidates = _agencyRepositoryCandidate.CandidatesForVacancy(vacancyId, p - 1, 10);
            TotalCandidates = _agencyRepositoryCandidate.TotalCandidatesForVacancy(vacancyId);
            Vacancy = _agencyRepositoryCandidate.Vacancy(year, month, title);
        }

        public ListCandidatesViewModel(IAgencyRepositoryCandidate _agencyRepositoryCandidate, int pageNo, string username)
        {
            Candidates = _agencyRepositoryCandidate.MyCandidates(pageNo - 1, 10, username);
            TotalCandidates = _agencyRepositoryCandidate.TotalMyCandidates(username);
        }

        public IList<Candidate> Candidates { get; private set; }
        public int TotalCandidates { get; private set; }

        public Vacancy Vacancy { get; private set; }
    }
}