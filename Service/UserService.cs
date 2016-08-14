using BL;
using BL.Workflow;
using NHibernate;
using RateIT.Example.DalMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Service
{
    //TODO: Stale Data
    //Make whole class generic
    public enum UserServiceResult {Success, StaleData }
    public class UserService : IEntityService<BL.User>
    {
        ISessionFactory _sessionFactory;
        public UserService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public IList<BL.User> List()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<User>()
                                  .Fetch(x => x.Workflows)
                                  .ToList();
            }
        }

        public void Save(BL.User user)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    EntityWorkflow<BL.User> workflow;
                    if (user.Id > 0)
                    {
                        workflow = new UpdateUserWorkflow(session.Load<User>(user.Id), user);
                        session.SaveOrUpdate(workflow.Owner);
                    }
                    else
                    {
                        workflow = new CreateUserWorkflow(user);
                    }
                    
                    session.Save(workflow);
                    transaction.Commit();
                }
            }
        }

        public UserServiceResult Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    BL.User user = session.Get<BL.User>(id, LockMode.Upgrade);

                    if(user.Status != EntityStatus.None)
                        return UserServiceResult.StaleData;

                    DeleteUserWorkflow deleteWorkflow = new DeleteUserWorkflow(user);
                    session.Save(deleteWorkflow);
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }
            return UserServiceResult.Success;
        }

        public BL.User Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                BL.Workflow.EntityWorkflow<User> workflowAlias = null;
                return session.QueryOver(() => userAlias)
                                  .Left.JoinAlias(() => userAlias.Workflows, () => workflowAlias)
                                  .Where(() => userAlias.Id == id)
                                  .List()
                                  .FirstOrDefault();
            }
        }
    }
}
