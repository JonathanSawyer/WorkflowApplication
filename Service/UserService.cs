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
    public class UserService : IEntityService<User>
    {
        ISessionFactory _sessionFactory;
        public UserService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public IList<User> List()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<User>()
                                  .Fetch(x => x.Workflows)
                                  .ToList();
            }
        }

        public void Save(User user)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    EntityWorkflow<User> workflow;
                    if (user.Id > 0)
                    {
                        workflow = new UserWorkflowUpdate(session.Load<User>(user.Id), user);
                        session.SaveOrUpdate(workflow.Owner);
                    }
                    else
                    {
                        workflow = new UserWorkflowCreate(user);
                    }
                    
                    session.Save(workflow);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    User user = session.Get<User>(id, LockMode.Upgrade);

                    UserWorkflowDelete deleteWorkflow = new UserWorkflowDelete(user);
                    session.Save(deleteWorkflow);
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }
        }

        public User Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                EntityWorkflow<User> workflowAlias = null;
                return session.QueryOver(() => userAlias)
                                  .Left.JoinAlias(() => userAlias.Workflows, () => workflowAlias)
                                  .Where(() => userAlias.Id == id)
                                  .List()
                                  .FirstOrDefault();
            }
        }
    }
}
