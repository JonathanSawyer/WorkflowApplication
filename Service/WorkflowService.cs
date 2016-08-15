using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;

namespace IdemWokflow.Service
{
    //TODO: Ensure all locking done correctly
    public class WorkflowService<T> : IWorkflowService<T> where T : PayloadEntity<T>
    {
        ISessionFactory _sessionFactory;
        public WorkflowService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public EntityWorkflow<T> Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<EntityWorkflow<T>>()
                              .Fetch(x => x.Owner)
                              .Where(x =>x.Id == id).FirstOrDefault();
            }
        }

        public IList<EntityWorkflow<T>> List()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<EntityWorkflow<T>>().List();
            }
        }

        public void Approve(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    //TODO: Needs to have an upgrade lock
                    EntityWorkflow<T> workflow = 
                                                            session.Query<EntityWorkflow<T>>()
                                                                   .Fetch(x => x.Owner)
                                                                   .Where(x => x.Id == id).FirstOrDefault();

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
                    EntityWorkflow<T> workflow = session.Get<EntityWorkflow<T>>(id, LockMode.Upgrade);
                    workflow.Reject();
                    
                    if (workflow.Owner != null)
                        session.SaveOrUpdate(workflow.Owner);

                    session.SaveOrUpdate(workflow);
                    transaction.Commit();
                }
            }
        }
    }
}
