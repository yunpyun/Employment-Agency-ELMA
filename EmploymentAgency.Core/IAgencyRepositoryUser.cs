using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepositoryUser
    {
        User User(string login, string pwd);
    }
}
