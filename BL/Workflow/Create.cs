using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public abstract class Create<T> : EntityWorkflow<T> where T : MyEntity<T>
    {
        public Create()
        {
            WorkflowStatus  = Workflow.WorkflowStatus.Pending;
            WorkflowType    = WorkflowType.Create;
        }
    }
}
