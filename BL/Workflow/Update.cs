using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public class Update<T> : EntityWorkflow<T> where T : MyEntity<T>
    {
        public Update()
        {
        }

        public Update(T item)
            : this()
        {
            if (item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition();

            Item = item;
            item.Status = EntityStatus.PendingUpdate;
            WorkflowStatus = Workflow.WorkflowStatus.Pending;
            WorkflowType = WorkflowType.Update;
        }
    }
}
