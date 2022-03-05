﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public class AgencyRepositoryCandidate : IAgencyRepositoryCandidate
    {
        // NHibernate object
        private readonly ISession _session;

        public AgencyRepositoryCandidate(ISession session)
        {
            _session = session;
        }

        // c - кандидаты

        public IList<Candidate> Candidates(int pageNo, int pageSize)
        {
            var candidates = _session.Query<Candidate>()
                                  .OrderByDescending(c => c.CandidatePostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            return candidates;
        }

        public int TotalCandidates()
        {
            return _session.Query<Candidate>().Count();
        }

        public Candidate Candidate(int year, int month, string title)
        {
            var query = _session.Query<Candidate>()
                                .Where(c => c.CandidatePostedOn.Year == year && c.CandidatePostedOn.Month == month && c.Title.Equals(title));

            return query.ToFuture().Single();
        }

        public IList<Candidate> CandidatesForVacancy(string workExperience, string requirements, int pageNo, int pageSize)
        {
            var candidates = _session.Query<Candidate>()
                                .Where(c => c.WorkExperience.Equals(workExperience) && c.Description.Contains(requirements))
                                .OrderByDescending(c => c.CandidatePostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            return candidates;
        }

        public int TotalCandidatesForVacancy(string workExperience, string requirements)
        {
            return _session.Query<Candidate>()
                        .Where(c => c.WorkExperience.Equals(workExperience) && c.Description.Contains(requirements))
                        .Count();
        }

        public Vacancy Vacancy(int year, int month, string title)
        {
            var query = _session.Query<Vacancy>()
                                .Where(v => v.VacancyPostedOn.Year == year && v.VacancyPostedOn.Month == month && v.Name.Equals(title));

            return query.ToFuture().Single();
        }
    }
}