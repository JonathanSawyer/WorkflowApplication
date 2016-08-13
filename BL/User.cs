using BL.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class User : MyEntity<User>
    {
        public User()
        {
            Status = EntityStatus.None;
            Workflows = new List<EntityWorkflow<User>>();
        }
        public virtual string Name { get; set; }
    }
}
