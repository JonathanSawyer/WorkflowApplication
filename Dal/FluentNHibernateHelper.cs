using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateIT.Example.DalMappings
{
    public static class FluentNHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(@"C:\firstProject.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()/*.ExportTo(@"C:\Development\RateITTest\MvcApplication1\Dal\GeneratedORMFiles\")*/)
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            //config.SetInterceptor(new SqlStatementInterceptor());
            // delete the existing db on each run
            if (File.Exists(@"C:\firstProject.db"))
                File.Delete(@"C:\firstProject.db");

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }

    }
}
