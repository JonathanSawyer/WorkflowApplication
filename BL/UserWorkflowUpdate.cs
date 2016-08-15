using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll
{
    public class UserWorkflowUpdate : Update<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowUpdate()
        {
            UserData = new UserData();
        }
        public UserWorkflowUpdate(User existing, User change)
            : base(existing)
        {
            UserData = new UserData();
            UserData.Update(change);
        }
        public override void Approve()
        {
            Owner.Status = EntityStatus.None;
            UserData.SetOwner(Owner);
            base.Approve();
        }
        public override void Reject()
        {
            Owner.Status = EntityStatus.None;
            base.Reject();
        }
    }
}
