using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll
{
    public class UserWorkflowDelete : Delete<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowDelete()
        {
            UserData = new UserData();
        }
        public UserWorkflowDelete(User user)
            : base(user)
        {
            UserData = new UserData();
            UserData.Update(user);
        }
        public override void Approve()
        {
            Owner.Status    = EntityStatus.None;
            Owner.Archived  = true;
            base.Approve();
        }
        public override void Reject()
        {
            Owner.Status = EntityStatus.None;
            base.Reject();
        }
    }
}
