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

        public IList<Vacancy> Vacancies(int pageNo, int pageSize)
        {
            var vacancies = _session.Query<Vacancy>()
                                  .OrderByDescending(p => p.VacancyPostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            var vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

            return _session.Query<Vacancy>()
                  .OrderByDescending(p => p.VacancyPostedOn)
                  .Where(p => vacancyIds.Contains(p.IdVacancy))
                  .ToList();
        }

        public int TotalVacancies()
        {
            return _session.Query<Vacancy>().Count();
        }

        public Vacancy Vacancy(int year, int month, string title)
        {
            var query = _session.Query<Vacancy>()
                                .Where(p => p.VacancyPostedOn.Year == year && p.VacancyPostedOn.Month == month && p.Name.Equals(title));

            return query.ToFuture().Single();
        }


        public IList<Candidate> Candidates(int pageNo, int pageSize)
        {
            var candidates = _session.Query<Candidate>()
                                  .OrderByDescending(p => p.CandidatePostedOn)
                                  .Skip(pageNo * pageSize)
                                  .Take(pageSize)
                                  .ToList();

            var candidateIds = candidates.Select(p => p.IdCandidate).ToList();

            return _session.Query<Candidate>()
                  .OrderByDescending(p => p.CandidatePostedOn)
                  .Where(p => candidateIds.Contains(p.IdCandidate))
                  .ToList();
        }

        public int TotalCandidates()
        {
            return _session.Query<Candidate>().Count();
        }

        public Candidate Candidate(int year, int month, string title)
        {
            var query = _session.Query<Candidate>()
                                .Where(p => p.CandidatePostedOn.Year == year && p.CandidatePostedOn.Month == month && p.Title.Equals(title));

            return query.ToFuture().Single();
        }

        public int AddVacancy(Vacancy vacancy)
        {
            using (var tran = _session.BeginTransaction())
            {
                _session.Save(vacancy);
                tran.Commit();
                return vacancy.IdVacancy;
            }
        }

        public void AddVacancyMSSQL(Vacancy vacancy)
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

        public IList<Candidate> CandidatesForVacancy(string workExperience, string requirements, int pageNo, int pageSize)
        {
            var candidates = _session.Query<Candidate>()
                                .Where(p => p.WorkExperience.Equals(workExperience) && p.Description.Contains(requirements))
                                .OrderByDescending(p => p.CandidatePostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            var candidateIds = candidates.Select(p => p.IdCandidate).ToList();

            return _session.Query<Candidate>()
                          .Where(p => candidateIds.Contains(p.IdCandidate))
                          .OrderByDescending(p => p.CandidatePostedOn)
                          .ToList();
        }

        public int TotalCandidatesForVacancy(string workExperience, string requirements)
        {
            return _session.Query<Candidate>()
                        .Where(p => p.WorkExperience.Equals(workExperience) && p.Description.Contains(requirements))
                        .Count();
        }

        public IList<Vacancy> VacanciesForCandidate(string workExperience, string requirements, int pageNo, int pageSize)
        {
            var vacancies = _session.Query<Vacancy>()
                                .Where(p => p.RequiredWorkExperience.Equals(workExperience) && requirements.Contains(p.Requirements))
                                .OrderByDescending(p => p.VacancyPostedOn)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .ToList();

            var vacancieIds = vacancies.Select(p => p.IdVacancy).ToList();

            return _session.Query<Vacancy>()
                          .Where(p => vacancieIds.Contains(p.IdVacancy))
                          .OrderByDescending(p => p.VacancyPostedOn)
                          .ToList();
        }

        public int TotalVacanciesForCandidate(string workExperience, string requirements)
        {
            return _session.Query<Vacancy>()
                        .Where(p => p.RequiredWorkExperience.Equals(workExperience) && requirements.Contains(p.Description))
                        .Count();
        }


        public IList<Vacancy> Vacancies(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Vacancy> vacancies;
            IList<int> vacancyIds;

            switch (sortColumn)
            {
                case "Name":
                    if (sortByAscending)
                    {
                        vacancies = _session.Query<Vacancy>()
                                        .OrderBy(p => p.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

                        vacancies = _session.Query<Vacancy>()
                                         .Where(p => vacancyIds.Contains(p.IdVacancy))
                                         .OrderBy(p => p.Name)
                                         .ToList();
                    }
                    else
                    {
                        vacancies = _session.Query<Vacancy>()
                                        .OrderByDescending(p => p.Name)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

                        vacancies = _session.Query<Vacancy>()
                                         .Where(p => vacancyIds.Contains(p.IdVacancy))
                                         .OrderByDescending(p => p.Name)
                                         .ToList();
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending)
                    {
                        vacancies = _session.Query<Vacancy>()
                                        .OrderBy(p => p.VacancyPostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

                        vacancies = _session.Query<Vacancy>()
                                         .Where(p => vacancyIds.Contains(p.IdVacancy))
                                         .OrderBy(p => p.VacancyPostedOn)
                                         .ToList();
                    }
                    else
                    {
                        vacancies = _session.Query<Vacancy>()
                                        .OrderByDescending(p => p.VacancyPostedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

                        vacancies = _session.Query<Vacancy>()
                                        .Where(p => vacancyIds.Contains(p.IdVacancy))
                                        .OrderByDescending(p => p.VacancyPostedOn)
                                        .ToList();
                    }
                    break;
                default:
                    vacancies = _session.Query<Vacancy>()
                                    .OrderByDescending(p => p.VacancyPostedOn)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                    vacancyIds = vacancies.Select(p => p.IdVacancy).ToList();

                    vacancies = _session.Query<Vacancy>()
                                     .Where(p => vacancyIds.Contains(p.IdVacancy))
                                     .OrderByDescending(p => p.VacancyPostedOn)
                                     .ToList();
                    break;
            }

            return vacancies;
        }

        public User User(string login, string pwd)
        {
            var query = _session.Query<User>()
                                .Where(p => p.Email.Equals(login) && p.Password.Equals(pwd));
            if (query.Count() > 0)
            {
                return query.ToFuture().Single();
            }
            else
            {
                return null;
            }
        }
    }
}
