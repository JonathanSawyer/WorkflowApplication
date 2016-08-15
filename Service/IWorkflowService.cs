using IdemWokflow.Bll.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Service
{
    public interface IWorkflowService<T>
    {
        EntityWorkflow<T> Get(int id);
        IList<EntityWorkflow<T>> List();
        void Approve(int id);
        void Reject(int id);
    }
}
