using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using EmploymentAgency.Core.Objects;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace EmploymentAgency.Core
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>()
                .ToMethod
                (
                    e =>
                        Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("EmploymentAgencyDbConnString")))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Vacancy>())
                        //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(false, true, false))
                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                        .BuildConfiguration()
                        .BuildSessionFactory()
                )
                .InSingletonScope();

            Bind<ISession>()
                .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }
    }
}

//.Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Server=DESKTOP-3THJ5UN\SQLEXPRESS; initial catalog=EmploymentAgencyELMA;"))
