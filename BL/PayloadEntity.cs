using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll
{
    public abstract class PayloadEntity<Entity> : Entity<int>
    {
        public virtual EntityStatus Status { get; set; }

        public virtual IList<EntityWorkflow<Entity>> Workflows { get; set; }
        public virtual bool Archived { get; set; }
    }
}
