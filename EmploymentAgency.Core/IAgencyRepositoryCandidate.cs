using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepositoryCandidate
    {
        /// <summary>
        /// Используется для возврата последних опубликованных резюме на основе значений разбиения на страницы
        /// </summary>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество резюме на одной странице</param>
        /// <returns>Список всех резюме</returns>
        IList<Candidate> Candidates(int pageNo, int pageSize);

        /// <summary>
        /// Используется для возврата количества всех резюме
        /// </summary>
        /// <returns>Число всех резюме</returns>
        int TotalCandidates();

        /// <summary>
        /// Используется для возвращения вакансии на основе года и месяца создания и заголовка
        /// </summary>
        /// <param name="year">Год публикации</param>
        /// <param name="month">Месяц публикации</param>
        /// <param name="title">Заголовок резюме</param>
        /// <returns>Конкретное резюме</returns>
        Candidate Candidate(int year, int month, string title);

        /// <summary>
        /// Используется для возвращения списка резюме, подходящих под конкретную вакансию, с разбивкой на страницы
        /// </summary>
        /// <param name="vacancyId">Идентификатор вакансии для поиска ее параметров</param>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество вакансий на одной странице</param>
        /// <returns>Список вакансий для отдельного резюме</returns>
        IList<Candidate> CandidatesForVacancy(int vacancyId, int pageNo, int pageSize);

        /// <summary>
        /// Используется для возврата числа общего количества резюме для конкретной вакансии
        /// </summary>
        /// <param name="vacancyId">Идентификатор вакансии для поиска ее параметров</param>
        /// <returns>Общее число резюме для вакансии</returns>
        int TotalCandidatesForVacancy(int vacancyId);

        /// <summary>
        /// Используется для возвращения вакансии на основе года и месяца создания и заголовка вакансии
        /// </summary>
        /// <param name="year">Год публикации</param>
        /// <param name="month">Месяц публикации</param>
        /// <param name="title">Заголовок вакансии</param>
        /// <returns>Конкретная вакансия</returns>
        Vacancy Vacancy(int year, int month, string title);

        /// <summary>
        /// Используется для возвращения резюме текущего пользователя
        /// </summary>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество резюме на одной странице</param>
        /// <param name="username">Имя текущего пользователя</param>
        /// <returns>Резюме текущего пользователя</returns>
        IList<Candidate> MyCandidates(int pageNo, int pageSize, string username);

        /// <summary>
        /// Используется для возвращения количества резюме текущего пользователя
        /// </summary>
        /// <param name="username">Имя текущего пользователя</param>
        /// <returns>Количество резюме текущего пользователя</returns>
        int TotalMyCandidates(string username);

        /// <summary>
        /// Используется для создания нового резюме
        /// </summary>
        /// <param name="candidate">Объект резюме</param>
        /// <param name="username">Имя текущего пользователя</param>
        void AddCandidate(Candidate candidate, string username);
    }
}
