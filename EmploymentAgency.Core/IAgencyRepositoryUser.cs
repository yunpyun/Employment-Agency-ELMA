using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentAgency.Core.Objects;

namespace EmploymentAgency.Core
{
    public interface IAgencyRepositoryUser
    {
        /// <summary>
        /// Используется для возвращения объекта пользователь или null, если пользователя с указанными кредами нет в системе
        /// </summary>
        /// <param name="login">Логин (почта) пользователя</param>
        /// <param name="pwd">Пароль пользователя</param>
        /// <returns>Объект пользователь или null</returns>
        UserAgency User(string login, string pwd);

        /// <summary>
        /// Используется для возвращения объекта пользователь или null, если пользователя с указанным email нет в системе
        /// </summary>
        /// <param name="login">Логин (почта) пользователя</param>
        /// <returns>Объект пользователь или null</returns>
        UserAgency UserForRegistration(string login);

        void AddUser(UserAgency user);
    }
}
