using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentAgency.Core.Objects
{
    public class Skill
    {
        public virtual int SkillId
        { get; set; }

        public virtual string Name
        { get; set; }

        public virtual IList<Vacancy> Vacancies
        { get; set; }

        public virtual IList<Candidate> Candidates
        { get; set; }
    }
}
