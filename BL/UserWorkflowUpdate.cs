using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserWorkflowUpdate : BL.Workflow.Update<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowUpdate()
        {
            UserData = new BL.UserData();
        }
        public UserWorkflowUpdate(User existing, User change)
            : base(existing)
        {
            UserData = new BL.UserData();
            UserData.Set(change);
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
