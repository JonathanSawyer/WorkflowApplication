using BL;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateIT.Example.DalMappings
{
    //TODO: Improve namespaces
    public class ErrorLogMap : ClassMap<ErrorLog>
    {
        public ErrorLogMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Stack);
            Map(x => x.ErrorDateTime);
            Map(x => x.ControllerContext);
        }
    }
}
