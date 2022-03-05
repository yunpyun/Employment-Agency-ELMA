﻿using System;
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
            Id(x => x.IdVacancy);

            Map(x => x.Name).Length(255).Not.Nullable();

            Map(x => x.Description).Length(4096).Not.Nullable();

            Map(x => x.TimePeriod).Length(255);

            Map(x => x.CompanyName).Length(255).Not.Nullable();

            Map(x => x.Requirements).Length(512).Not.Nullable();

            Map(x => x.Salary).Not.Nullable();

            Map(x => x.RequiredWorkExperience).Length(255).Not.Nullable(); ;

            Map(x => x.Address).Length(512).Not.Nullable(); ;

            Map(x => x.VacancyPostedOn).Not.Nullable();
        }
    }
}
