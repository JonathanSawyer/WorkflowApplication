using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateIT.Example.DalMappings
{
    public class UserBaseClassMap : ClassMap<EntityWorkflow<BL.User>>
    {
        public UserBaseClassMap()
        {
            Table("UserWorkflow");
            DiscriminateSubClassesOnColumn("Type");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.WorkflowStatus,    "Status");
            Map(x => x.WorkflowType,      "Type").ReadOnly();
            References(x => x.Owner).Column("UserId");
        }
    }

    public class CreateUserMap : SubclassMap<CreateUserWorkflow>
    {
        public CreateUserMap()
        {
            DiscriminatorValue(WorkflowType.Create);
            Map(x => x.Name, "Name");
        }
    }

    public class DeleteUserMap : SubclassMap<DeleteUserWorkflow>
    {
        public DeleteUserMap()
        {
            DiscriminatorValue(WorkflowType.Delete);
            Map(x => x.Name, "Name");
        }
    }

    public class UpdateUserMap : SubclassMap<UpdateUserWorkflow>
    {
        public UpdateUserMap()
        {
            DiscriminatorValue(WorkflowType.Update);
            Map(x => x.Name, "Name");
        }
    }
}
