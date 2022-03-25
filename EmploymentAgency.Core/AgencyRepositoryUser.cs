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

        public IList<UserAgency> Users()
        {
            var users = _session.Query<UserAgency>()
                                  .OrderByDescending(u => u.Email)
                                  .Fetch(v => v.Role)
                                  .ToList();

            return users;
        }

        public int TotalUsers()
        {
            return _session.Query<UserAgency>().Count();
        }

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
            var role = _session.Query<Role>()
                                .Where(r => r.Name.Equals(user.RoleWanted.Name))
                                .ToFuture().SingleOrDefault();

            _session.CreateSQLQuery("exec proc_AddUser :pEmail, :pPassword, :pFirstName, :pMiddleName, :pLastName, :pRole, :pRoleWanted")
                    .AddEntity(typeof(UserAgency))
                    .SetParameter("pEmail", user.Email)
                    .SetParameter("pPassword", user.Password)
                    .SetParameter("pFirstName", user.FirstName)
                    .SetParameter("pMiddleName", user.MiddleName)
                    .SetParameter("pLastName", user.LastName)
                    .SetParameter("pRole", 3)
                    .SetParameter("pRoleWanted", role.RoleId)
                    .List<UserAgency>();
        }

        public UserAgency UserForEdit(int userId)
        {
            var query = _session.Query<UserAgency>()
                                .Where(u => u.UserId.Equals(userId));

            return query.ToFuture().SingleOrDefault();
        }

        public void EditUser(UserAgency user)
        {
            var role = _session.Query<Role>()
                                .Where(r => r.Name.Equals(user.Role.Name))
                                .ToFuture().SingleOrDefault();

            _session.CreateSQLQuery("exec proc_EditUser :pUserId, :pEmail, :pPassword, :pFirstName, :pMiddleName, :pLastName, :pRole")
                    .AddEntity(typeof(UserAgency))
                    .SetParameter("pUserId", user.UserId)
                    .SetParameter("pEmail", user.Email)
                    .SetParameter("pPassword", user.Password)
                    .SetParameter("pFirstName", user.FirstName)
                    .SetParameter("pMiddleName", user.MiddleName)
                    .SetParameter("pLastName", user.LastName)
                    .SetParameter("pRole", role.RoleId)
                    .List<UserAgency>();
        }

        public void ApproveRole(UserAgency user)
        {
            user.Role = user.RoleWanted;

            var role = _session.Query<Role>()
                                .Where(r => r.Name.Equals(user.Role.Name))
                                .ToFuture().SingleOrDefault();

            _session.CreateSQLQuery("exec proc_EditUser :pUserId, :pEmail, :pPassword, :pFirstName, :pMiddleName, :pLastName, :pRole")
                    .AddEntity(typeof(UserAgency))
                    .SetParameter("pUserId", user.UserId)
                    .SetParameter("pEmail", user.Email)
                    .SetParameter("pPassword", user.Password)
                    .SetParameter("pFirstName", user.FirstName)
                    .SetParameter("pMiddleName", user.MiddleName)
                    .SetParameter("pLastName", user.LastName)
                    .SetParameter("pRole", role.RoleId)
                    .List<UserAgency>();
        }
    }
}
