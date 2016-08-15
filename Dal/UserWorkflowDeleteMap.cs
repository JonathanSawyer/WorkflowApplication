
using BL;
using BL.Workflow;
using FluentNHibernate.Mapping;

namespace Workflow.DalMapping
{
    public class UserWorkflowDeleteMap : SubclassMap<UserWorkflowDelete>
    {
        public UserWorkflowDeleteMap()
        {
            DiscriminatorValue(WorkflowType.Delete);
            Component(x => x.UserData, m =>
            {
                UserBaseClassMap.InitializeUserData(m);
            });
        }
    }
}
