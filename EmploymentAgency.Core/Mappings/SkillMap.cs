using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    public class SkillMap : ClassMap<Skill>
    {
        public SkillMap()
        {
            Id(x => x.SkillId);

            Map(x => x.Name).Length(255).Not.Nullable();

            HasManyToMany(x => x.Vacancies).Cascade.All().Inverse().Table("VacancySkillMap");

            HasManyToMany(x => x.Candidates).Cascade.All().Inverse().Table("CandidateSkillMap");
        }
    }
}
