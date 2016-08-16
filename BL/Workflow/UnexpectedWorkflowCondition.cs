using System;
namespace IdemWokflow.Bll.Workflow
{
    public class UnexpectedWorkflowCondition : Exception
    {
        //public UnexpectedWorkflowCondition(){}
        public UnexpectedWorkflowCondition(string message) : base(message)
        {
        }
    }
}
