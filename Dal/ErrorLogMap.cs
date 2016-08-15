using FluentNHibernate.Mapping;
using IdemWokflow.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Dal
{
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
