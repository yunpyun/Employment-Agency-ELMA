using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmploymentAgency.Core.Objects
{
    public class Vacancy
    {
        public virtual int IdVacancy
        { get; set; }

        [Required(ErrorMessage = "Поле \"Название вакансии\" обязательно к заполнению")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "В поле \"Название вакансии\" должно быть не менее 3 и не более 100 символов")]
        [Display(Name = "Название вакансии*")]
        public virtual string Name
        { get; set; }

        [Required(ErrorMessage = "Поле \"Описание вакансии\" обязательно к заполнению")]
        [StringLength(4000, MinimumLength = 10, ErrorMessage = "В поле \"Описание вакансии\" должно быть не менее 10 и не более 4000 символов")]
        [Display(Name = "Описание вакансии*")]
        [DataType(DataType.MultilineText)]
        public virtual string Description
        { get; set; }

        [StringLength(100, ErrorMessage = "В поле \"Период, на который ищется работник\" должно быть не более 100 символов")]
        [Display(Name = "Период, на который ищется работник")]
        public virtual string TimePeriod
        { get; set; }

        [Required(ErrorMessage = "Поле \"Название компании\" обязательно к заполнению")]
        [StringLength(200, ErrorMessage = "В поле \"Название компании\" должно быть не более 200 символов")]
        [Display(Name = "Название компании*")]
        public virtual string CompanyName
        { get; set; }

        [Required(ErrorMessage = "Поле \"Требования\" обязательно к заполнению")]
        [StringLength(300, ErrorMessage = "В поле \"Требования\" должно быть не более 300 символов")]
        [Display(Name = "Требования*")]
        public virtual string Requirements
        { get; set; }

        [Required(ErrorMessage = "Поле \"Заработная плата\" обязательно к заполнению")]
        [Display(Name = "Заработная плата*")]
        public virtual decimal Salary
        { get; set; }

        [Required(ErrorMessage = "Поле \"Требуемый опыт работы\" обязательно к заполнению")]
        [StringLength(100, ErrorMessage = "В поле \"Требуемый опыт работы\" должно быть не более 100 символов")]
        [Display(Name = "Требуемый опыт работы*")]
        public virtual string RequiredWorkExperience
        { get; set; }

        [Required(ErrorMessage = "Поле \"Адрес компании\" обязательно к заполнению")]
        [StringLength(300, ErrorMessage = "В поле \"Адрес компании\" должно быть не более 300 символов")]
        [Display(Name = "Адрес компании*")]
        public virtual string Address
        { get; set; }

        public virtual DateTime VacancyPostedOn
        { get; set; }

        public virtual User Author
        { get; set; }
    }
}
