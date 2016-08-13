using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    //Workflows are idempotent
    public class Delete<T> : EntityWorkflow<T> where T : MyEntity<T>
    {
        public Delete()
        {}

        public Delete(T item)
            : this()
        {
            if(item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition();
            
            WorkflowStatus  = Workflow.WorkflowStatus.Pending;
            WorkflowType    = WorkflowType.Delete;
        }
    }
}
