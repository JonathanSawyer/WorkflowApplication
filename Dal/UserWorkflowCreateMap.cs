using FluentNHibernate.Mapping;
using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;

namespace IdemWokflow.Dal
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
