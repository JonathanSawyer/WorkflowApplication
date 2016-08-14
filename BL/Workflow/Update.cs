using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public abstract class Update<T> : EntityWorkflow<T> where T : MyEntity<T>
    {
        public Update()
        {
        }

        public Update(T item)
            : this()
        {
            if (item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition();

            Owner = item;
            Owner.Status = EntityStatus.PendingUpdate;
            Owner.Workflows.Add(this);
            WorkflowStatus = Workflow.WorkflowStatus.Pending;
            WorkflowType = WorkflowType.Update;
        }
    }
}
