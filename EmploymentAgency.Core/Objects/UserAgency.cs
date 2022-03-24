using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmploymentAgency.Core.Objects
{
    public class UserAgency
    {
        public virtual int UserId
        { get; set; }

        [Required]
        [Display(Name = "Логин/Email")]
        public virtual string Email 
        { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public virtual string Password 
        { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public virtual string FirstName
        { get; set; }

        [Display(Name = "Отчество")]
        public virtual string MiddleName
        { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public virtual string LastName
        { get; set; }

        [Display(Name = "Роль")]
        public virtual Role Role
        { get; set; }

        [Required]
        [Display(Name = "Желаемая роль")]
        public virtual Role RoleWanted
        { get; set; }

        public virtual IList<Candidate> Candidates
        { get; set; }

        public virtual IList<Vacancy> Vacancies
        { get; set; }
    }
}
