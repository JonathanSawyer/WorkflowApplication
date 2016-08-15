using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DalMapping
{
    public class UserMap : ClassMap<User>
    {
       public UserMap()
       {
           Id(x => x.Id).GeneratedBy.Identity();
           Map(x => x.Name);
           Map(x => x.Surname);
           Map(x => x.Status);
           Map(x => x.Archived, "Archived");
           HasMany(x => x.Workflows)
               .KeyColumn("UserId")
               .Inverse()
               .Cascade
               .SaveUpdate();
           Where("Archived is not 1");
       }
    }
}
