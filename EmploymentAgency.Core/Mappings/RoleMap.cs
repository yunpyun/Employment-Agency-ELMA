using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.RoleId);

            Map(x => x.Name).Length(255).Not.Nullable();

            HasMany(x => x.UserAgency)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Role");
        }
    }
}
