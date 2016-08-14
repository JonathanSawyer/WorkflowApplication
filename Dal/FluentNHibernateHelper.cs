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
                .Database(SQLiteConfiguration.Standard.UsingFile(@"C:\WorkflowApplication.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()/*.ExportTo(@"C:\Development\RateITTest\MvcApplication1\Dal\GeneratedORMFiles\")*/)
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(@"C:\WorkflowApplication.db"))
                File.Delete(@"C:\WorkflowApplication.db");

            new SchemaExport(config)
              .Create(false, true);
        }

    }
}
