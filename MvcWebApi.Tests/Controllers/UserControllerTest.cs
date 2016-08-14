using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcWebApi;
using MvcWebApi.Controllers;
using Service;
using NHibernate;
using RateIT.Example.DalMappings;
using BL;
using BL.Workflow;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;

namespace MvcWebApi.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        ISessionFactory _sessionFactory;

        [ClassInitialize()]
        public static void ClassInit(TestContext context) 
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestInitialize()]
        public void Initialize() 
        {
            _sessionFactory = FluentNHibernateHelper.CreateSessionFactory();
        }

        [TestCleanup()]
        public void Cleanup() {}

        [ClassCleanup()]
        public static void ClassCleanup() {}

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
            IEnumerable<dynamic> result = controller.Get();
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
        public void Post_Create()
        {
            User user = new User()
            {
                Name = "Some Name"
            };
            UserController controller = new UserController(new UserService(_sessionFactory));
            controller.Post(user);

            CreateUserWorkflow workflow;
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                CreateUserWorkflow workflowAlias = null;
                workflow = session.QueryOver(() => workflowAlias)
                                  .Left.JoinAlias(() => workflowAlias.Owner, () => userAlias)
                                  .List()
                                  .FirstOrDefault();
            }
            Assert.IsNotNull(workflow);
            Assert.IsNull(workflow.Owner);
            Assert.AreEqual("Some Name", workflow.UserData.Name);
            Assert.AreEqual(WorkflowStatus.Pending, workflow.WorkflowStatus);
            Assert.AreEqual(WorkflowType.Create, workflow.WorkflowType);
        }

        [TestMethod]
        public void Post_Update()
        {
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
            user.Name = "Updated Value";
            UserController controller = new UserController(new UserService(_sessionFactory));
            controller.Post(user);

            UpdateUserWorkflow workflow;
            using (var session = _sessionFactory.OpenSession())
            {
                User userAlias = null;
                UpdateUserWorkflow workflowAlias = null;
                workflow = session.QueryOver(() => workflowAlias)
                                  .Left.JoinAlias(() => workflowAlias.Owner, () => userAlias)
                                  .List()
                                  .FirstOrDefault();
            }
            Assert.IsNotNull(workflow);
            Assert.IsNotNull(workflow.Owner);
            Assert.AreEqual(user.Id, workflow.Owner.Id);
            Assert.AreEqual("Some Name", workflow.Owner.Name);
            Assert.AreEqual("Updated Value", workflow.UserData.Name);
            Assert.AreEqual(WorkflowStatus.Pending, workflow.WorkflowStatus);
            Assert.AreEqual(WorkflowType.Update, workflow.WorkflowType);
        }

        [TestMethod]
        public void Delete()
        {
            User user = new User()
            {
                Name   = "Some Name"
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
            UserServiceResult result = controller.Delete(user.Id);
            Assert.AreEqual(UserServiceResult.Success, result);
            
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
                                  .Inner.JoinAlias(() => workflowAlias.Owner, () => userAlias)
                                  .List()
                                  .FirstOrDefault();
            }
            Assert.IsNotNull(workflow);
            Assert.IsInstanceOfType(workflow, typeof(Delete<User>));
            Assert.AreEqual(user.Id,   workflow.Owner.Id);
            Assert.AreEqual(user.Name, workflow.Owner.Name);
            Assert.AreEqual(EntityStatus.PendingDelete, workflow.Owner.Status);
            Assert.AreEqual(WorkflowStatus.Pending, workflow.WorkflowStatus);
            Assert.AreEqual(WorkflowType.Delete, workflow.WorkflowType);
        }
    }
}
