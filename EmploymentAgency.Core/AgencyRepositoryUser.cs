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
    public class AgencyRepositoryUser : IAgencyRepositoryUser
    {
        // NHibernate object
        private readonly ISession _session;

        public AgencyRepositoryUser(ISession session)
        {
            _session = session;
        }

        // u - пользователи

        public UserAgency User(string login, string pwd)
        {
            var query = _session.Query<UserAgency>()
                                .Where(u => u.Email.Equals(login) && u.Password.Equals(pwd));

            return query.ToFuture().SingleOrDefault();
        }

        public UserAgency UserForRegistration(string login)
        {
            var query = _session.Query<UserAgency>()
                                .Where(u => u.Email.Equals(login));

            return query.ToFuture().SingleOrDefault();
        }

        public void AddUser(UserAgency user)
        {
            _session.CreateSQLQuery("exec proc_AddUser :pEmail, :pPassword, :pFirstName, :pMiddleName, :pLastName, :pRole")
                    .AddEntity(typeof(UserAgency))
                    .SetParameter("pEmail", user.Email)
                    .SetParameter("pPassword", user.Password)
                    .SetParameter("pFirstName", user.FirstName)
                    .SetParameter("pMiddleName", user.MiddleName)
                    .SetParameter("pLastName", user.LastName)
                    .SetParameter("pRole", 3)
                    .List<UserAgency>();
        }
    }
}
