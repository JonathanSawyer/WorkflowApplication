using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll.Workflow
{
    public abstract class Create<T> : EntityWorkflow<T> where T : PayloadEntity<T>
    {
        public Create()
        {
            WorkflowStatus  = Workflow.WorkflowStatus.Pending;
            WorkflowType    = WorkflowType.Create;
        }
    }
}
