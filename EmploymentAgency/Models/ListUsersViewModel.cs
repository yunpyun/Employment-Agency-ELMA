using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Models
{
    public class ListUsersViewModel
    {
        public ListUsersViewModel(IAgencyRepositoryUser _agencyRepositoryUser)
        {
            Users = _agencyRepositoryUser.Users();
            TotalUsers = _agencyRepositoryUser.TotalUsers();
        }

        public IList<UserAgency> Users { get; private set; }
        public int TotalUsers { get; private set; }
    }
}