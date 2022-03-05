using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmploymentAgency.Core;
using EmploymentAgency.Core.Objects;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EmploymentAgency
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString VacancyLink(this HtmlHelper helper, Vacancy vacancy)
        {
            return helper.ActionLink(vacancy.Name, "Vacancy", "Agency",
                new
                {
                    year = vacancy.VacancyPostedOn.Year,
                    month = vacancy.VacancyPostedOn.Month,
                    title = vacancy.Name
                },
                new
                {
                    title = vacancy.Name
                });
        }

        public static MvcHtmlString CandidateLink(this HtmlHelper helper, Candidate candidate)
        {
            return helper.ActionLink(candidate.Title, "Candidate", "AgencyCandidate",
                new
                {
                    year = candidate.CandidatePostedOn.Year,
                    month = candidate.CandidatePostedOn.Month,
                    title = candidate.Title
                },
                new
                {
                    title = candidate.Title
                });
        }

        public static MvcHtmlString CandidatesForVacancyLink(this HtmlHelper helper, Vacancy vacancy)
        {
            return helper.ActionLink("Показать подходящие резюме", "CandidatesForVacancy", "AgencyCandidate", 
                new 
                {
                    workExperience = vacancy.RequiredWorkExperience,
                    requirements = vacancy.Requirements,
                    year = vacancy.VacancyPostedOn.Year,
                    month = vacancy.VacancyPostedOn.Month,
                    title = vacancy.Name
                }, 
                new 
                { 
                    title = String.Format("See all candidates in {0}", vacancy.Name) 
                });
        }

        public static MvcHtmlString VacanciesForCandidateLink(this HtmlHelper helper, Candidate candidate)
        {
            return helper.ActionLink("Показать подходящие вакансии", "VacanciesForCandidate", "Agency",
                new
                {
                    workExperience = candidate.WorkExperience,
                    requirements = candidate.Description,
                    year = candidate.CandidatePostedOn.Year,
                    month = candidate.CandidatePostedOn.Month,
                    title = candidate.Title
                },
                new
                {
                    title = String.Format("See all candidates in {0}", candidate.Title)
                });
        }

    }
}