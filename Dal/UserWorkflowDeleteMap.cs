using FluentNHibernate.Mapping;
using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;

namespace IdemWokflow.Dal
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
