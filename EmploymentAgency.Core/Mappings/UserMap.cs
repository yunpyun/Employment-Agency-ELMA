using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.IdUser);

            Map(x => x.Email).Length(100).Not.Nullable();

            Map(x => x.Password).Length(55).Not.Nullable();
        }
    }
}
