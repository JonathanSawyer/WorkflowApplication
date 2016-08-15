using FluentNHibernate.Mapping;
using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;

namespace IdemWokflow.Dal
{
    public class UserWorkflowUpdateMap : SubclassMap<UserWorkflowUpdate>
    {
        public UserWorkflowUpdateMap()
        {
            DiscriminatorValue(WorkflowType.Update);
            Component(x => x.UserData, m =>
            {
                UserBaseClassMap.InitializeUserData(m);
            });
        }
    }
}
