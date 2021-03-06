﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Bll.Workflow
{
    public abstract class Update<T> : EntityWorkflow<T> where T : PayloadEntity<T>
    {
        public Update()
        {
        }

        public Update(T item)
            : this()
        {
            if (item.Status != EntityStatus.None)
                throw new UnexpectedWorkflowCondition("Update request failed because record is already in pending state.");

            Owner = item;
            Owner.Status = EntityStatus.PendingUpdate;
            Owner.Workflows.Add(this);
            WorkflowStatus = Workflow.WorkflowStatus.Pending;
            WorkflowType = WorkflowType.Update;
        }
    }
}
