using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepository
    {
        /// <summary>
        /// Используется для возврата последних опубликованных вакансий на основе значений разбиения на страницы
        /// </summary>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество вакансий на одной странице</param>
        /// <returns>Список всех вакансий</returns>
        IList<Vacancy> Vacancies(int pageNo, int pageSize);

        /// <summary>
        /// Используется для возврата количества всех вакансии
        /// </summary>
        /// <returns>Число всех вакансий</returns>
        int TotalVacancies();

        /// <summary>
        /// Используется для возвращения вакансии на основе года и месяца создания и заголовка
        /// </summary>
        /// <param name="year">Год публикации</param>
        /// <param name="month">Месяц публикации</param>
        /// <param name="title">Заголовок вакансии</param>
        /// <returns>Конкретная вакансия</returns>
        Vacancy Vacancy(int year, int month, string title);

        /// <summary>
        /// Используется для возвращения списка вакансий, подходящих под конкретное резюме, с разбивкой на страницы
        /// </summary>
        /// <param name="candidateId">Идентификатор резюме для поиска его параметров</param>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество вакансий на одной странице</param>
        /// <returns>Список вакансий для отдельного резюме</returns>
        IList<Vacancy> VacanciesForCandidate(int candidateId, int pageNo, int pageSize);

        /// <summary>
        /// Используется для возврата числа общего количества вакансии для конкретного резюме
        /// </summary>
        /// <param name="candidateId">Идентификатор резюме для поиска его параметров</param>
        /// <returns>Общее число вакансий для резюме</returns>
        int TotalVacanciesForCandidate(int candidateId);

        /// <summary>
        /// Используется для возвращения резюме на основе года и месяца создания и заголовка резюме
        /// </summary>
        /// <param name="year">Год публикации</param>
        /// <param name="month">Месяц публикации</param>
        /// <param name="title">Заголовок резюме</param>
        /// <returns>Конкретное резюме</returns>
        Candidate Candidate(int year, int month, string title);

        /// <summary>
        /// Используется для возвращения отсортированного списка вакансий
        /// </summary>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество вакансий на одной странице</param>
        /// <param name="sortColumn">Параметр для сортировки</param>
        /// <param name="sortByAscending">Сортировать ли в порядке возрастания</param>
        /// <returns>Отсортированные вакансии</returns>
        IList<Vacancy> VacanciesSort(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

        /// <summary>
        /// Используется для возвращения вакансий текущего пользователя
        /// </summary>
        /// <param name="pageNo">Номер страницы</param>
        /// <param name="pageSize">Количество вакансий на одной странице</param>
        /// <param name="username">Имя текущего пользователя</param>
        /// <returns>Вакансии текущего пользователя</returns>
        IList<Vacancy> MyVacancies(int pageNo, int pageSize, string username);

        /// <summary>
        /// Используется для возвращения количества вакансий текущего пользователя
        /// </summary>
        /// <param name="username">Имя текущего пользователя</param>
        /// <returns>Количество вакансий текущего пользователя</returns>
        int TotalMyVacancies(string username);

        /// <summary>
        /// Используется для создания новой вакансии
        /// </summary>
        /// <param name="vacancy">Объект вакансии</param>
        /// <param name="username">Имя текущего пользователя</param>
        void AddVacancy(Vacancy vacancy, string username);

    }
}
