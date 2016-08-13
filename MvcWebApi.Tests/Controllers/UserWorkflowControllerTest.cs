using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using RateIT.Example.DalMappings;
using MvcWebApi.Controllers;
using Service;
using BL;
using System.Collections.Generic;

namespace MvcWebApi.Tests.Controllers
{
    [TestClass]
    public class UserWorkflowControllerTest
    {
        ISessionFactory         _sessionFactory;
        IEntityService<User>    _userService;
        IWorkflowService<User>  _userWorkflowService;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestInitialize()]
        public void Initialize()
        {
            _sessionFactory      = FluentNHibernateHelper.CreateSessionFactory();
            _userService         = new UserService(_sessionFactory);
            _userWorkflowService = new WorkflowService<User>(_sessionFactory);
        }

        [TestCleanup()]
        public void Cleanup() 
        { 
        }

        [ClassCleanup()]
        public static void ClassCleanup() { }

        [TestMethod]
        public void Get()
        {
            User user1 = new BL.User()
            {
                Name = "Some user"
            };
            User user2 = new BL.User()
            {
                Name = "Some other user"
            };
            _userService.Save(user1);
            _userService.Save(user2);

            UserWorkflowController controller                     = new UserWorkflowController(_userService, _userWorkflowService);
            IList<BL.Workflow.EntityWorkflow<User>> userWorkflows = controller.Get();

            Assert.AreEqual(2,          userWorkflows.Count);
            Assert.AreEqual(user1.Name, ((CreateUserWorkflow)userWorkflows[0]).UserData.Name);
            Assert.AreEqual(user2.Name, ((CreateUserWorkflow)userWorkflows[1]).UserData.Name);

        }

        [TestMethod]
        public void GetById()
        {
            User user1 = new BL.User()
            {
                Name = "Some user"
            };
            User user2 = new BL.User()
            {
                Name = "Some other user"
            };
            _userService.Save(user1);
            _userService.Save(user2);
            
            UserWorkflowController controller = new UserWorkflowController(_userService, _userWorkflowService);
            CreateUserWorkflow userWorkflow1 = (CreateUserWorkflow)controller.Get(1);
            CreateUserWorkflow userWorkflow2 = (CreateUserWorkflow)controller.Get(2);

            Assert.AreEqual(user1.Name, userWorkflow1.UserData.Name);
            Assert.AreEqual(user2.Name, userWorkflow2.UserData.Name);

        }

        [TestMethod]
        public void Approve()
        {

        }

        [TestMethod]
        public void Reject()
        {

        }

    }
}
