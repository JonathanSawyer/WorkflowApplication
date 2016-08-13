using BL;
using NHibernate;
using RateIT.Example.DalMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
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
                return session.QueryOver<BL.User>().List();
            }
        }

        public void Save(BL.User item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(item);
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

                    BL.Workflow.Delete<User> deleteWorkflow = new BL.Workflow.Delete<User>(user);
                    deleteWorkflow.Item = user;
                    user.Status         = EntityStatus.PendingDelete;
                    user.Workflows.Add(deleteWorkflow);

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
                return session.Get<BL.User>(id);
            }
        }
    }
}
