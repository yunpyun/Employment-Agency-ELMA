using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    class UserMap : ClassMap<UserAgency>
    {
        public UserMap()
        {
            Id(x => x.UserId);

            Map(x => x.Email).Length(255).Not.Nullable();

            Map(x => x.Password).Length(255).Not.Nullable();

            Map(x => x.FirstName).Length(255).Not.Nullable();

            Map(x => x.MiddleName).Length(255);

            Map(x => x.LastName).Length(255).Not.Nullable();

            References(x => x.Role).Column("Role").Not.Nullable();

            HasMany(x => x.Candidates)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Author");

            HasMany(x => x.Vacancies)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Author");
        }
    }
}
