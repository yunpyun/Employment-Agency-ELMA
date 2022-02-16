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
            Id(x => x.IdCandidate);

            Map(x => x.FirstName).Length(50).Not.Nullable();

            Map(x => x.MiddleName).Length(55);

            Map(x => x.LastName).Length(100).Not.Nullable();

            Map(x => x.Birthday).Not.Nullable();

            Map(x => x.WorkExperience).Length(100).Not.Nullable();

            Map(x => x.Photo);

            Map(x => x.Phone).Length(50).Not.Nullable();

            Map(x => x.Email).Length(50).Not.Nullable(); ;

            Map(x => x.CandidatePostedOn).Not.Nullable();

            Map(x => x.Title).Length(100).Not.Nullable();

            Map(x => x.Description).Length(4000).Not.Nullable();
        }
    }
}
