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
            Id(x => x.UserId);

            Map(x => x.Email).Length(255).Not.Nullable();

            Map(x => x.Password).Length(255).Not.Nullable();

            Map(x => x.Role).Length(255).Not.Nullable();

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
