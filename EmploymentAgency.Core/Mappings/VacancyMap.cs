using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    public class VacancyMap : ClassMap<Vacancy>
    {
        public VacancyMap()
        {
            Id(x => x.VacancyId);

            Map(x => x.Name).Length(255).Not.Nullable();

            Map(x => x.Description).Length(4000).Not.Nullable();

            Map(x => x.CompanyName).Length(255).Not.Nullable();

            Map(x => x.Salary).Not.Nullable();

            Map(x => x.TimePeriod).Length(255);

            Map(x => x.RequiredWorkExperience).Not.Nullable();

            //Map(x => x.Requirements).Length(512).Not.Nullable();

            HasManyToMany(x => x.Skills).Table("VacancySkillMap");

            Map(x => x.Address).Length(512).Not.Nullable(); ;

            References(x => x.Status).Column("Status").Not.Nullable();

            References(x => x.Author).Column("Author").Not.Nullable();

            Map(x => x.VacancyPostedOn).Not.Nullable();
        }
    }
}
