using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public abstract class EntityWorkflow<T> : Entity<int>
    {
        public virtual T Owner { get; set; }
        public EntityWorkflow()
        {}
        public virtual WorkflowType   WorkflowType   { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }

        public virtual void Approve()
        {
            if (WorkflowStatus == Workflow.WorkflowStatus.Pending)
                WorkflowStatus = WorkflowStatus.Approved;
            else
                throw new UnexpectedWorkflowCondition();
        }

        public virtual void Reject()
        {
            if (WorkflowStatus == Workflow.WorkflowStatus.Pending)
                WorkflowStatus = WorkflowStatus.Rejected;
            else
                throw new UnexpectedWorkflowCondition();
        }
    }
}
