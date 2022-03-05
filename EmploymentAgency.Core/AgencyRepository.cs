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
                                  .ToList();

            return vacancies;
        }

        public int TotalVacancies()
        {
            return _session.Query<Vacancy>().Count();
        }

        public Vacancy Vacancy(int year, int month, string title)
        {
            var query = _session.Query<Vacancy>()
                                .Where(v => v.VacancyPostedOn.Year == year && v.VacancyPostedOn.Month == month && v.Name.Equals(title));

            return query.ToFuture().Single();
        }

        public IList<Vacancy> VacanciesForCandidate(string workExperience, string requirements, int pageNo, int pageSize)
        {
            var vacancies = _session.Query<Vacancy>()
                                .Where(v => v.RequiredWorkExperience.Equals(workExperience) && requirements.Contains(v.Requirements))
                                .OrderByDescending(v => v.VacancyPostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            return vacancies;
        }

        public int TotalVacanciesForCandidate(string workExperience, string requirements)
        {
            return _session.Query<Vacancy>()
                        .Where(v => v.RequiredWorkExperience.Equals(workExperience) && requirements.Contains(v.Description))
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

            vacancies = _session.Query<Vacancy>()
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            switch (sortColumn)
            {
                case "Name":
                    sort = v => v.Name;
                    vacancies = sortByAscending ? vacancies.OrderBy(sort).ToList() : vacancies.OrderByDescending(sort).ToList();
                    break;
                case "PostedOn":
                    sort = v => v.VacancyPostedOn;
                    vacancies = sortByAscending ? vacancies.OrderBy(sort).ToList() : vacancies.OrderByDescending(sort).ToList();
                    break;
                default:
                    sort = v => v.VacancyPostedOn;
                    vacancies = vacancies.OrderByDescending(sort).ToList();
                    break;
            }

            return vacancies;
        }

        public void AddVacancy(Vacancy vacancy)
        {
            AddVacancyMSSQL(vacancy);
        }

        // создание вакансии с помощью вызова хранимой процедуры из БД
        private void AddVacancyMSSQL(Vacancy vacancy)
        {
            _session.CreateSQLQuery("exec proc_AddVacancy :pVacancyName, :pDescription, :pTimePeriod, :pCompanyName, :pRequirements, :pSalary, :pRequiredWorkExperience, :pAddress, :pVacancyPostedOn")
                    .AddEntity(typeof(Vacancy))
                    .SetParameter("pVacancyName", vacancy.Name)
                    .SetParameter("pDescription", vacancy.Description)
                    .SetParameter("pTimePeriod", vacancy.TimePeriod)
                    .SetParameter("pCompanyName", vacancy.CompanyName)
                    .SetParameter("pRequirements", vacancy.Requirements)
                    .SetParameter("pSalary", vacancy.Salary)
                    .SetParameter("pRequiredWorkExperience", vacancy.RequiredWorkExperience)
                    .SetParameter("pAddress", vacancy.Address)
                    .SetParameter("pVacancyPostedOn", DateTime.Now)
                    .List<Vacancy>();
        }

    }
}
