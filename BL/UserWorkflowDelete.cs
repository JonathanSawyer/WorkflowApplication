using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserWorkflowDelete : BL.Workflow.Delete<User>, IUserData
    {
        public virtual UserData UserData { get; set; }
        public UserWorkflowDelete()
        {
            UserData = new BL.UserData();
        }
        public UserWorkflowDelete(User user)
            : base(user)
        {
            UserData = new BL.UserData();
            UserData.Update(user);
        }
    }
}
