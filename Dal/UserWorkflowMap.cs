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
    //TODO: Remove redundancy
    public class UserBaseClassMap : ClassMap<EntityWorkflow<BL.User>>
    {
        public UserBaseClassMap()
        {
            Table("UserWorkflow");
            DiscriminateSubClassesOnColumn("Type");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.WorkflowStatus,    "Status");
            Map(x => x.MakerDateTime);
            Map(x => x.ApproverDateTime);
            Map(x => x.WorkflowType,      "Type").ReadOnly();
            References(x => x.Owner).Column("UserId");
        }
    }

    public class CreateUserMap : SubclassMap<CreateUserWorkflow>
    {
        public CreateUserMap()
        {
            DiscriminatorValue(WorkflowType.Create);
            Component(x => x.UserData, m =>
            {
                m.Map(x => x.Name, "Name");
                m.Map(x => x.Surname, "Surname");
            });
        }
    }

    public class DeleteUserMap : SubclassMap<DeleteUserWorkflow>
    {
        public DeleteUserMap()
        {
            DiscriminatorValue(WorkflowType.Delete);
            Component(x => x.UserData, m =>
            {
                m.Map(x => x.Name, "Name");
                m.Map(x => x.Surname, "Surname");
            });
        }
    }

    public class UpdateUserMap : SubclassMap<UpdateUserWorkflow>
    {
        public UpdateUserMap()
        {
            DiscriminatorValue(WorkflowType.Update);
            Component(x => x.UserData, m =>
            {
                m.Map(x => x.Name, "Name");
                m.Map(x => x.Surname, "Surname");
            });
        }
    }
}
