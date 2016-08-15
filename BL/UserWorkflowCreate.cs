using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll
{
    public class UserWorkflowCreate : Create<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowCreate()
        {
            UserData = new UserData();
        }
        public UserWorkflowCreate(User user)
            : base()
        {
            UserData = new UserData();
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
