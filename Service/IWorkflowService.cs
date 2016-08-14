using BL.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IWorkflowService<T>
    {
        BL.Workflow.EntityWorkflow<T> Get(int id);
        IList<BL.Workflow.EntityWorkflow<T>> List();
        void Approve(int id);
        void Reject(int id);
    }
}
