using BL.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserData
    {
        public virtual string Name { get; set; }
        public UserData()
        { }
        public void Set(User user)
        {
            Name = user.Name;
        }

        public void SetOwner(User owner)
        {
            owner.Name = Name;
        }
    }
    public class CreateUserWorkflow : BL.Workflow.Create<User>
    {
        public virtual UserData UserData { get; set; }
        public CreateUserWorkflow() 
        {
            UserData = new BL.UserData();
        }
        public CreateUserWorkflow(User user) 
            : base()
        {
            UserData = new BL.UserData();
            UserData.Set(user);
        }

        public override void Approve()
        {
            Owner = new User();
            UserData.SetOwner(Owner);
            base.Approve();
        }
    }
    public class DeleteUserWorkflow : BL.Workflow.Delete<User>
    {
        public virtual UserData UserData { get; set; }
        public DeleteUserWorkflow() 
        {
            UserData = new BL.UserData();
        }
        public DeleteUserWorkflow(User user) 
            : base(user)
        {
            UserData = new BL.UserData();
            UserData.Set(user);
        }
    }
    public class UpdateUserWorkflow : BL.Workflow.Update<User>
    {
        public virtual UserData UserData { get; set; }
        public UpdateUserWorkflow() 
        {
            UserData = new BL.UserData();
        }
        public UpdateUserWorkflow(User existing, User change)
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
    }
}
