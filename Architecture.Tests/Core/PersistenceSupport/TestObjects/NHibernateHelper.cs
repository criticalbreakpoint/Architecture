using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Architecture.Tests.Core.PersistenceSupport.TestObjects
{
    public class NHibernateHelper
    {
        private static readonly ISessionFactory SessionFactory = Configuration.BuildSessionFactory();
        private static Configuration _configuration;

        protected static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = Fluently
                        .Configure()
                        .Database(
                            () => SQLiteConfiguration.Standard
                                      .InMemory()
                                      .ShowSql())
                        .Mappings(
                            x => x.FluentMappings
                                     .AddFromAssemblyOf
                                     <NHibernateHelper>())
                        .BuildConfiguration();
                }

                return _configuration;
            }
        }

        public static ISession OpenSession()
        {
            ISession session = SessionFactory.OpenSession();

            var export = new SchemaExport(Configuration);
            export.Execute(false, true, false, session.Connection, null);

            return session;
        }
    }
}