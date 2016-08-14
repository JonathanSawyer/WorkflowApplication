﻿using BL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    //TODO: Ensure all locking done correctly
    //Add user and date time stamp for approvals.
    public class WorkflowService<T> : IWorkflowService<T> where T : MyEntity<T>
    {
        ISessionFactory _sessionFactory;
        public WorkflowService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public BL.Workflow.EntityWorkflow<T> Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                BL.Workflow.EntityWorkflow<T> workflowAlias = null;
                return session.QueryOver(() => workflowAlias)
                                  .Left.JoinAlias(() => workflowAlias.Owner, () => userAlias)
                                  .Where(() => workflowAlias.Id == id)
                                  .List()
                                  .FirstOrDefault();
            }
        }

        public IList<BL.Workflow.EntityWorkflow<T>> List()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<BL.Workflow.EntityWorkflow<T>>().List();
            }
        }

        public void Approve(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    User userAlias = null;
                    BL.Workflow.EntityWorkflow<T> workflowAlias = null;
                    //TODO: Needs to have an upgrade lock
                    BL.Workflow.EntityWorkflow<T> workflow = session.QueryOver(() => workflowAlias)
                                      .Left.JoinAlias(() => workflowAlias.Owner, () => userAlias)
                                      .Where(() => workflowAlias.Id == id)
                                      .List()
                                      .FirstOrDefault();

                    workflow.Approve();

                    session.SaveOrUpdate(workflow.Owner);
                    session.SaveOrUpdate(workflow);

                    transaction.Commit();
                }
            }
        }

        public void Reject(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    BL.Workflow.EntityWorkflow<T> workflow = session.Get<BL.Workflow.EntityWorkflow<T>>(id, LockMode.Upgrade);
                    workflow.Reject();
                    session.Save(workflow);
                    transaction.Commit();
                }
            }
        }
    }
}