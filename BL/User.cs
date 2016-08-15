using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll
{
    public class User : PayloadEntity<User>
    {
        public User()
        {
            Status = EntityStatus.None;
            Workflows = new List<EntityWorkflow<User>>();
        }
        public virtual string Name   { get; set; }
        public virtual string Surname { get; set; }

    }
}
