using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public class Create<T> : EntityWorkflow<T> where T : MyEntity<T>
    {
        public Create()
        {}

        public Create(T item) : base(item) 
        {
            if (item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition();

            Item           = item;
            item.Status    = EntityStatus.PendingCreate;
            WorkflowStatus = Workflow.WorkflowStatus.Pending;
            WorkflowType   = WorkflowType.Create;
        }
    }
}
