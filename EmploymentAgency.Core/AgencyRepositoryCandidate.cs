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

        public IList<Candidate> CandidatesForVacancy(int vacancyId, int pageNo, int pageSize)
        {
            int workExperience;
            string requirements;
            var query = _session.Query<Vacancy>()
                                .Where(u => u.IdVacancy.Equals(vacancyId));

            workExperience = query.ToFuture().Single().RequiredWorkExperience;
            requirements = query.ToFuture().Single().Requirements;

            string[] requirementsArr = requirements.Split(','); 

            var candidates = _session.Query<Candidate>()
                                .Where(c => (workExperience >= ((DateTime.Today.Month < c.StartWork.Month) ? (DateTime.Today.Year - c.StartWork.Year) - 1 : (DateTime.Today.Year - c.StartWork.Year))) && requirementsArr.All(c.Description.Contains))
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
            string requirements;
            var query = _session.Query<Vacancy>()
                                .Where(u => u.IdVacancy.Equals(vacancyId));

            workExperience = query.ToFuture().Single().RequiredWorkExperience;
            requirements = query.ToFuture().Single().Requirements;

            string[] requirementsArr = requirements.Split(',');

            return _session.Query<Candidate>()
                        .Where(c => (workExperience >= ((DateTime.Today.Month < c.StartWork.Month) ? (DateTime.Today.Year - c.StartWork.Year) - 1 : (DateTime.Today.Year - c.StartWork.Year))) && requirementsArr.All(c.Description.Contains))
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

            userId = query.ToFuture().Single().IdUser;

            var candidates = _session.Query<Candidate>()
                                  .Where(c => c.Author.IdUser == userId)
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

            userId = query.ToFuture().Single().IdUser;

            return _session.Query<Candidate>()
                        .Where(c => c.Author.IdUser == userId)
                        .Count();
        }
    }
}
