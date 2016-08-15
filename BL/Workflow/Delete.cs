using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll.Workflow
{
    public abstract class Delete<T> : EntityWorkflow<T> where T : PayloadEntity<T>
    {
        public Delete()
        {}

        public Delete(T item)
            : this()
        {
            if(item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition();
            
            Owner = item;
            item.Status = EntityStatus.PendingDelete;
            item.Workflows.Add(this);
            WorkflowStatus  = Workflow.WorkflowStatus.Pending;
            WorkflowType    = WorkflowType.Delete;
        }
    }
}
