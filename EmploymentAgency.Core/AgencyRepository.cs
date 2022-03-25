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
    public class AgencyRepository : IAgencyRepository
    {
        // NHibernate object
        private readonly ISession _session;

        public AgencyRepository(ISession session)
        {
            _session = session;
        }

        // v - вакансии

        public IList<Vacancy> Vacancies(int pageNo, int pageSize)
        {
            var vacancies = _session.Query<Vacancy>()
                                  .OrderByDescending(v => v.VacancyPostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .Fetch(v => v.Author)
                                  .Fetch(v => v.Status)
                                  .ToList();

            var vacancyIds = vacancies.Select(v => v.VacancyId).ToList();

            return _session.Query<Vacancy>()
                  .Where(v => vacancyIds.Contains(v.VacancyId))
                  .OrderByDescending(v => v.VacancyPostedOn)
                  .FetchMany(v => v.Skills)
                  .ToList();
        }

        public int TotalVacancies()
        {
            var vacancies = _session.Query<Vacancy>()
                                  .OrderByDescending(v => v.VacancyPostedOn)
                                  .Fetch(v => v.Author)
                                  .Fetch(v => v.Status)
                                  .ToList();

            var vacancyIds = vacancies.Select(v => v.VacancyId).ToList();

            return _session.Query<Vacancy>()
                  .Where(v => vacancyIds.Contains(v.VacancyId) && v.Status.Name == "Активный")
                  .OrderByDescending(v => v.VacancyPostedOn)
                  .FetchMany(v => v.Skills)
                  .Count();

            //return _session.Query<Vacancy>().Count();
        }

        public Vacancy Vacancy(int year, int month, string title)
        {
            var query = _session.Query<Vacancy>()
                                .Where(v => v.VacancyPostedOn.Year == year && v.VacancyPostedOn.Month == month && v.Name.Equals(title));

            return query.ToFuture().Single();
        }

        public IList<Vacancy> VacanciesForCandidate(int candidateId, int pageNo, int pageSize)
        {
            DateTime startWork;
            string description;
            var query = _session.Query<Candidate>()
                                .Where(u => u.CandidateId.Equals(candidateId));

            startWork = query.ToFuture().Single().StartWork;
            description = query.ToFuture().Single().Description;

            var vacancies = _session.Query<Vacancy>()
                                .Where(v => v.RequiredWorkExperience <= ((DateTime.Today.Month < startWork.Month) ? (DateTime.Today.Year - startWork.Year) - 1 : (DateTime.Today.Year - startWork.Year)))
                                .OrderByDescending(v => v.VacancyPostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Fetch(v => v.Author)
                                .ToList();

            return vacancies;
        }

        public int TotalVacanciesForCandidate(int candidateId)
        {
            DateTime startWork;
            string description;
            var query = _session.Query<Candidate>()
                                .Where(u => u.CandidateId.Equals(candidateId));

            startWork = query.ToFuture().Single().StartWork;
            description = query.ToFuture().Single().Description;

            return _session.Query<Vacancy>()
                        .Where(v => v.RequiredWorkExperience <= ((DateTime.Today.Month < startWork.Month) ? (DateTime.Today.Year - startWork.Year) - 1 : (DateTime.Today.Year - startWork.Year)))
                        .Count();
        }

        public Candidate Candidate(int year, int month, string title)
        {
            var query = _session.Query<Candidate>()
                                .Where(c => c.CandidatePostedOn.Year == year && c.CandidatePostedOn.Month == month && c.Title.Equals(title));

            return query.ToFuture().Single();
        }

        public IList<Vacancy> VacanciesSort(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Vacancy> vacancies;
            Func<Vacancy, object> sort;

            switch (sortColumn)
            {
                case "Name":
                    sort = v => v.Name;
                    break;
                case "PostedOn":
                    sort = v => v.VacancyPostedOn;
                    break;
                default:
                    sort = v => v.VacancyPostedOn;
                    break;
            }

            vacancies = _session.Query<Vacancy>()
                            .Skip(pageNo * pageSize)
                            .Take(pageSize)
                            .Fetch(v => v.Author)
                            .ToList();

            vacancies = sortByAscending ? vacancies.OrderBy(sort).ToList() : vacancies.OrderByDescending(sort).ToList();

            return vacancies;
        }

        public IList<Vacancy> MyVacancies(int pageNo, int pageSize, string username)
        {
            int userId;
            var query = _session.Query<UserAgency>()
                                .Where(u => u.Email.Equals(username));

            userId = query.ToFuture().SingleOrDefault().UserId;

            var vacancies = _session.Query<Vacancy>()
                              .Where(v => v.Author.UserId == userId)
                              .OrderByDescending(v => v.VacancyPostedOn)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Fetch(v => v.Author)
                              .ToList();

            return vacancies;
        }

        public int TotalMyVacancies(string username)
        {
            int userId;
            var query = _session.Query<UserAgency>()
                                .Where(u => u.Email.Equals(username));

            userId = query.ToFuture().SingleOrDefault().UserId;

            return _session.Query<Vacancy>()
                    .Where(v => v.Author.UserId == userId)
                    .Count();
        }

        /// <inheritdoc/>
        public void AddVacancy(Vacancy vacancy, string username)
        {
            AddVacancyMSSQL(vacancy, username);
        }

        // создание вакансии с помощью вызова хранимой процедуры из БД
        private void AddVacancyMSSQL(Vacancy vacancy, string username)
        {
            int userId;
            var query = _session.Query<UserAgency>()
                                .Where(u => u.Email.Equals(username));

            userId = query.ToFuture().Single().UserId;

            _session.CreateSQLQuery("exec proc_AddVacancy :pVacancyName, :pDescription, :pTimePeriod, :pCompanyName, :pSalary, :pRequiredWorkExperience, :pAddress, :pVacancyPostedOn, :pAuthor")
                    .AddEntity(typeof(Vacancy))
                    .SetParameter("pVacancyName", vacancy.Name)
                    .SetParameter("pDescription", vacancy.Description)
                    .SetParameter("pTimePeriod", vacancy.TimePeriod)
                    .SetParameter("pCompanyName", vacancy.CompanyName)
                    .SetParameter("pSalary", vacancy.Salary)
                    .SetParameter("pRequiredWorkExperience", vacancy.RequiredWorkExperience)
                    .SetParameter("pAddress", vacancy.Address)
                    .SetParameter("pVacancyPostedOn", DateTime.Now)
                    .SetParameter("pAuthor", userId)
                    .List<Vacancy>();
        }

    }
}
