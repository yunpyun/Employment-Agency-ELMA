using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmploymentAgency.Core.Objects
{
    public class Candidate
    {
        public virtual int CandidateId
        { get; set; }

        [Required(ErrorMessage = "Поле \"Имя\" обязательно к заполнению")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "В поле \"Имя\" должно быть не менее 3 и не более 50 символов")]
        [Display(Name = "Имя*")]
        public virtual string FirstName
        { get; set; }

        [StringLength(55, MinimumLength = 3, ErrorMessage = "В поле \"Имя\" должно быть не менее 3 и не более 50 символов")]
        [Display(Name = "Отчество")]
        public virtual string MiddleName
        { get; set; }

        [Required(ErrorMessage = "Поле \"Фамилия\" обязательно к заполнению")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "В поле \"Имя\" должно быть не менее 3 и не более 50 символов")]
        [Display(Name = "Фамилия*")]
        public virtual string LastName
        { get; set; }

        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "Поле \"День Рождения\" обязательно к заполнению")]
        [Display(Name = "День Рождения*")]
        public virtual DateTime Birthday
        { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "В поле \"Фото\" должно быть не менее 3 и не более 255 символов")]
        [Display(Name = "Фото")]
        public virtual string Photo
        { get; set; }

        [Required(ErrorMessage = "Поле \"Телефон\" обязательно к заполнению")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "В поле \"Телефон\" должно быть не менее 6 и не более 50 символов")]
        [Display(Name = "Телефон*")]
        public virtual string Phone
        { get; set; }

        [Required(ErrorMessage = "Поле \"Email\" обязательно к заполнению")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "В поле \"Email\" должно быть не менее 6 и не более 50 символов")]
        [Display(Name = "Email*")]
        public virtual string Email
        { get; set; }

        [Display(Name = "Начало работы")]
        public virtual DateTime StartWork
        { get; set; }

        [Required(ErrorMessage = "Поле \"Название резюме\" обязательно к заполнению")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "В поле \"Название резюме\" должно быть не менее 3 и не более 100 символов")]
        [Display(Name = "Название резюме*")]
        public virtual string Title
        { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Поле \"Описание\" обязательно к заполнению")]
        [StringLength(4000, MinimumLength = 3, ErrorMessage = "В поле \"Описание\" должно быть не менее 3 и не более 4000 символов")]
        [Display(Name = "Описание*")]
        public virtual string Description
        { get; set; }

        public virtual IList<Skill> Skills
        { get; set; }

        public virtual UserAgency Author
        { get; set; }

        public virtual DateTime CandidatePostedOn
        { get; set; }
    }
}
