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
    public class AgencyRepositoryUser : IAgencyRepositoryUser
    {
        // NHibernate object
        private readonly ISession _session;

        public AgencyRepositoryUser(ISession session)
        {
            _session = session;
        }

        // u - пользователи

        public User User(string login, string pwd)
        {
            var query = _session.Query<User>()
                                .Where(u => u.Email.Equals(login) && u.Password.Equals(pwd));

            return query.ToFuture().SingleOrDefault();
        }
    }
}
