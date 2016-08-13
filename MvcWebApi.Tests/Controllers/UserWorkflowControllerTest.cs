using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using RateIT.Example.DalMappings;
using MvcWebApi.Controllers;
using Service;
using BL;
using System.Collections.Generic;
using BL.Workflow;

namespace MvcWebApi.Tests.Controllers
{
    [TestClass]
    public class UserWorkflowControllerTest
    {
        ISessionFactory         _sessionFactory;
        IEntityService<User>    _userService;
        IWorkflowService<User>  _userWorkflowService;
        UserWorkflowController  _userWorkflowController;
        User _user1;
        User _user2;

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
            _userWorkflowController = new UserWorkflowController(_userService, _userWorkflowService);

            _user1 = new BL.User()
            {
                Name = "Some user"
            };
            _user2 = new BL.User()
            {
                Name = "Some other user"
            };
            _userService.Save(_user1);
            _userService.Save(_user2);
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
            IList<BL.Workflow.EntityWorkflow<User>> userWorkflows = _userWorkflowController.Get();

            Assert.AreEqual(2,          userWorkflows.Count);
            Assert.AreEqual(_user1.Name, ((CreateUserWorkflow)userWorkflows[0]).UserData.Name);
            Assert.AreEqual(_user2.Name, ((CreateUserWorkflow)userWorkflows[1]).UserData.Name);

        }

        [TestMethod]
        public void GetById()
        {
            CreateUserWorkflow userWorkflow1 = (CreateUserWorkflow)_userWorkflowController.Get(1);
            CreateUserWorkflow userWorkflow2 = (CreateUserWorkflow)_userWorkflowController.Get(2);

            Assert.AreEqual(_user1.Name, userWorkflow1.UserData.Name);
            Assert.AreEqual(_user2.Name, userWorkflow2.UserData.Name);

        }

        [TestMethod]
        public void Approve_Create()
        {
            UserWorkflowController controller = new UserWorkflowController(_userService, _userWorkflowService);
            controller.Approve(1);
            CreateUserWorkflow userWorkflow1 = (CreateUserWorkflow)_userWorkflowController.Get(1);
            Assert.AreEqual(WorkflowStatus.Approved, userWorkflow1.WorkflowStatus);
            Assert.IsNotNull(userWorkflow1.Owner);
            Assert.AreEqual(userWorkflow1.UserData.Name, userWorkflow1.Owner.Name);
            Assert.AreEqual(EntityStatus.None, userWorkflow1.Owner.Status);
        }

        [TestMethod]
        public void Approve_Update()
        {
            UserWorkflowController controller = new UserWorkflowController(_userService, _userWorkflowService);
            controller.Approve(1);
            CreateUserWorkflow userWorkflow1 = (CreateUserWorkflow)_userWorkflowController.Get(1);
            Assert.AreEqual(WorkflowStatus.Approved, userWorkflow1.WorkflowStatus);
        }

        [TestMethod]
        public void Approve_Delete()
        {

        }

        [TestMethod]
        public void Reject()
        {

        }
    }
}
