using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserWorkflowCreate : BL.Workflow.Create<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowCreate()
        {
            UserData = new BL.UserData();
        }
        public UserWorkflowCreate(User user)
            : base()
        {
            UserData = new BL.UserData();
            UserData.Update(user);
        }

        public override void Approve()
        {
            Owner = new User();
            UserData.SetOwner(Owner);
            base.Approve();
        }
    }
}
