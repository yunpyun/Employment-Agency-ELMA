using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core.Mappings
{
    public class CandidateMap : ClassMap<Candidate>
    {
        public CandidateMap()
        {
            Id(x => x.CandidateId);

            Map(x => x.FirstName).Length(255).Not.Nullable();

            Map(x => x.MiddleName).Length(255);

            Map(x => x.LastName).Length(255).Not.Nullable();

            Map(x => x.Birthday).Not.Nullable();

            Map(x => x.Photo);

            Map(x => x.Phone).Length(255).Not.Nullable();

            Map(x => x.Email).Length(255).Not.Nullable();

            Map(x => x.StartWork);

            Map(x => x.Title).Length(255).Not.Nullable();

            Map(x => x.Description).Length(4000).Not.Nullable();

            HasManyToMany(x => x.Skills).Table("CandidateSkillMap");

            References(x => x.Author).Column("Author").Not.Nullable();

            Map(x => x.CandidatePostedOn).Not.Nullable();
        }
    }
}
