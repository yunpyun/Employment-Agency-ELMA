using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentAgency.Core.Objects
{
    public class Role
    {
        public virtual int RoleId
        { get; set; }

        public virtual string Name
        { get; set; }

        public virtual IList<UserAgency> UserAgency
        { get; set; }
    }
}
