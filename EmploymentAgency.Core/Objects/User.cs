using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmploymentAgency.Core.Objects
{
    public class User
    {
        public virtual int IdUser
        { get; set; }

        [Required]
        public virtual string Email 
        { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public virtual string Password 
        { get; set; }

        public virtual string Role
        { get; set; }

        public virtual IList<Candidate> Candidates
        { get; set; }

        public virtual IList<Vacancy> Vacancies
        { get; set; }
    }
}
