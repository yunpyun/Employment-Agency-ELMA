using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    public class StatusMap : ClassMap<Status>
    {
        public StatusMap()
        {
            Id(x => x.StatusId);

            Map(x => x.Name).Length(255).Not.Nullable();

            HasMany(x => x.Vacancies)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Status");
        }
    }
}
