using System;
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
                                  .Fetch(c => c.Author)
                                  .ToList();

            var candidateIds = candidates.Select(c => c.CandidateId).ToList();

            return _session.Query<Candidate>()
                  .Where(c => candidateIds.Contains(c.CandidateId))
                  .OrderByDescending(c => c.CandidatePostedOn)
                  .FetchMany(c => c.Skills)
                  .ToList();
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

        public IList<Candidate> CandidatesForVacancy(int vacancyId, int pageNo, int pageSize)
        {
            int workExperience;
            var query = _session.Query<Vacancy>()
                                .Where(u => u.VacancyId.Equals(vacancyId));

            workExperience = query.ToFuture().Single().RequiredWorkExperience;

            var candidates = _session.Query<Candidate>()
                                .Where(c => (workExperience >= ((DateTime.Today.Month < c.StartWork.Month) ? (DateTime.Today.Year - c.StartWork.Year) - 1 : (DateTime.Today.Year - c.StartWork.Year))))
                                .OrderByDescending(c => c.CandidatePostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Fetch(c => c.Author)
                                .ToList();

            return candidates;
        }

        public int TotalCandidatesForVacancy(int vacancyId)
        {
            int workExperience;
            var query = _session.Query<Vacancy>()
                                .Where(u => u.VacancyId.Equals(vacancyId));

            workExperience = query.ToFuture().Single().RequiredWorkExperience;

            return _session.Query<Candidate>()
                        .Where(c => (workExperience >= ((DateTime.Today.Month < c.StartWork.Month) ? (DateTime.Today.Year - c.StartWork.Year) - 1 : (DateTime.Today.Year - c.StartWork.Year))))
                        .Count();
        }

        public Vacancy Vacancy(int year, int month, string title)
        {
            var query = _session.Query<Vacancy>()
                                .Where(v => v.VacancyPostedOn.Year == year && v.VacancyPostedOn.Month == month && v.Name.Equals(title));

            return query.ToFuture().Single();
        }

        public IList<Candidate> MyCandidates(int pageNo, int pageSize, string username)
        {
            int userId;
            var query = _session.Query<User>()
                                .Where(u => u.Email.Equals(username));

            userId = query.ToFuture().Single().UserId;

            var candidates = _session.Query<Candidate>()
                                  .Where(c => c.Author.UserId == userId)
                                  .OrderByDescending(c => c.CandidatePostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .Fetch(c => c.Author)
                                  .ToList();

            return candidates;
        }

        public int TotalMyCandidates(string username)
        {
            int userId;
            var query = _session.Query<User>()
                                .Where(u => u.Email.Equals(username));

            userId = query.ToFuture().Single().UserId;

            return _session.Query<Candidate>()
                        .Where(c => c.Author.UserId == userId)
                        .Count();
        }
    }
}
