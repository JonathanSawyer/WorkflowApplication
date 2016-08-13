using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApplication1;
using MvcApplication1.Controllers;
using Service;
using NHibernate;
using RateIT.Example.DalMappings;
using BL;
using BL.Workflow;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;

namespace MvcApplication1.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        //TODO: Use a diferent database for test
        //   -> TestcaseSource
        [TestMethod]
        public void Get()
        {
            ISessionFactory _sessionFactory = FluentNHibernateHelper.CreateSessionFactory();
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(new User()
                    {
                        Name = "Some Name"
                    });
                    session.SaveOrUpdate(new User()
                    {
                        Name = "Some Other Name"
                    });
                    transaction.Commit();
                }
            }

            UserController controller = new UserController(new UserService(_sessionFactory));
            IList<BL.User> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Some Name", result.ElementAt(0).Name);
            Assert.AreEqual("Some Other Name", result.ElementAt(1).Name);
        }

        [TestMethod]
        public void GetById()
        {
            ISessionFactory _sessionFactory = FluentNHibernateHelper.CreateSessionFactory();
            User user = new User()
            {
                Name = "Some Name"
            };
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }

            UserController controller = new UserController(new UserService(_sessionFactory));

            User result = controller.Get(user.Id);

            Assert.AreEqual("Some Name", result.Name);
        }

        [TestMethod]
        public void Post()
        {
            ISessionFactory _sessionFactory = FluentNHibernateHelper.CreateSessionFactory();
            UserController controller = new UserController(new UserService(_sessionFactory));
            controller.Post(new BL.User()
            {
                Name = "Something"
            });
            User result = new User();
            using (var session = _sessionFactory.OpenSession())
            {
                result = session.QueryOver<BL.User>().List().FirstOrDefault();
            }
            Assert.IsNotNull(result);
            Assert.AreEqual("Something", result.Name);

        }

        [TestMethod]
        public void Delete()
        {
            log4net.Config.XmlConfigurator.Configure();
            ISessionFactory _sessionFactory = FluentNHibernateHelper.CreateSessionFactory();

            User user = new User()
            {
                Name = "Some Name"
            };
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }

            UserController controller = new UserController(new UserService(_sessionFactory));
            controller.Delete(user.Id);
            
            using (var session = _sessionFactory.OpenSession())
            {
                Assert.IsNotNull(session.Get<User>(user.Id));
            }
            EntityWorkflow<User> workflow;
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                EntityWorkflow<User> workflowAlias = null;
                workflow = session.QueryOver(() => workflowAlias)
                                  .Inner.JoinAlias(() => workflowAlias.Item, () => userAlias)
                                  .List()
                                  .FirstOrDefault();
            }
            Assert.IsNotNull(workflow);
            Assert.IsInstanceOfType(workflow, typeof(Delete<User>));
            Assert.AreEqual(user.Id,   workflow.Item.Id);
            Assert.AreEqual(user.Name, workflow.Item.Name);
            Assert.AreEqual(EntityStatus.PendingDelete, workflow.Item.Status);

        }
    }
}
