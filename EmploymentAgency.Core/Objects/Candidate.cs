using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentAgency.Core.Objects
{
    public class Candidate
    {
        public virtual int CandidateId
        { get; set; }

        public virtual string FirstName
        { get; set; }

        public virtual string MiddleName
        { get; set; }

        public virtual string LastName
        { get; set; }

        public virtual DateTime Birthday
        { get; set; }

        public virtual string Photo
        { get; set; }

        public virtual string Phone
        { get; set; }

        public virtual string Email
        { get; set; }

        public virtual DateTime StartWork
        { get; set; }

        public virtual string Title
        { get; set; }

        public virtual string Description
        { get; set; }

        public virtual IList<Skill> Skills
        { get; set; }

        public virtual User Author
        { get; set; }

        public virtual DateTime CandidatePostedOn
        { get; set; }
    }
}
