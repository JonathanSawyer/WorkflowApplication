using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll.Workflow
{
    public abstract class EntityWorkflow<T> : Entity<int>
    {
        public virtual T Owner { get; set; }
        public EntityWorkflow()
        {
            MakerDateTime = DateTime.Now;
        }
        public virtual WorkflowType   WorkflowType   { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        public virtual DateTime? MakerDateTime       { get; set; }
        public virtual DateTime? ApproverDateTime    { get; set; }

        public virtual void Approve()
        {
            if (WorkflowStatus == Workflow.WorkflowStatus.Pending)
            {
                WorkflowStatus = WorkflowStatus.Approved;
                ApproverDateTime = DateTime.Now;
            }
            else
                throw new UnexpectedWorkflowCondition("Approval failed because workflow is no longer pending.");
        }

        public virtual void Reject()
        {
            if (WorkflowStatus == Workflow.WorkflowStatus.Pending)
            {
                WorkflowStatus = WorkflowStatus.Rejected;
                ApproverDateTime = DateTime.Now;
            }
            else
                throw new UnexpectedWorkflowCondition("Reject failed because workflow is no longer pending.");
        }
    }
}
