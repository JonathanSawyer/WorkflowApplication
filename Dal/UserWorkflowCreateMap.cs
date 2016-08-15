using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;

namespace Workflow.DalMapping
{
    public class UserWorkflowCreateMap : SubclassMap<UserWorkflowCreate>
    {
        public UserWorkflowCreateMap()
        {
            DiscriminatorValue(WorkflowType.Create);
            Component(x => x.UserData, m =>
            {
                UserBaseClassMap.InitializeUserData(m);
            });
        }
    }
}
