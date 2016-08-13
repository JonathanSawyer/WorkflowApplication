using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Workflow
{
    public abstract class EntityWorkflow<T> : Entity<int>
    {
        public virtual T Item { get; set; }
        public EntityWorkflow()
        {}
        public EntityWorkflow(T item)
        {
            Item = item;
        }
        public virtual WorkflowType   WorkflowType   { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }

        protected virtual void Approve()
        {
            WorkflowStatus = WorkflowStatus.Approved;
        }

        protected virtual void Reject()
        {
            WorkflowStatus = WorkflowStatus.Rejected;
        }
    }
}
