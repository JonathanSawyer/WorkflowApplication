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
            Map(x => x.WorkflowStatus, "Status");
            Map(x => x.WorkflowType, "Type").ReadOnly();
            References(x => x.Item).Column("UserId");
        }
    }

    public class CreateUserMap : SubclassMap<Create<BL.User>>
    {
        public CreateUserMap()
        {
            DiscriminatorValue(WorkflowType.Create);
        }
    }

    public class DeleteUserMap : SubclassMap<Delete<BL.User>>
    {
        public DeleteUserMap()
        {
            DiscriminatorValue(WorkflowType.Delete);
        }
    }

    public class UpdateUserMap : SubclassMap<Update<BL.User>>
    {
        public UpdateUserMap()
        {
            DiscriminatorValue(WorkflowType.Update);
        }
    }
}
