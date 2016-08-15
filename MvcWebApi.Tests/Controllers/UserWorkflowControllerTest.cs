using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using MvcWebApi.Controllers;
using Service;
using BL;
using System.Collections.Generic;
using BL.Workflow;
using System.Linq;
using Workflow.DalMapping;

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
            //dynamic userWorkflows = _userWorkflowController.Get();
            //Assert.AreEqual(2, userWorkflows.Count);
            //Assert.AreEqual(_user1.Name, userWorkflows[0].Name);
            //Assert.AreEqual(_user2.Name, userWorkflows[1].Name);
        }

        [TestMethod]
        public void GetById()
        {
            UserWorkflowCreate userWorkflow1 = (UserWorkflowCreate)_userWorkflowController.Get(1);
            UserWorkflowCreate userWorkflow2 = (UserWorkflowCreate)_userWorkflowController.Get(2);

            Assert.AreEqual(_user1.Name, userWorkflow1.UserData.Name);
            Assert.AreEqual(_user2.Name, userWorkflow2.UserData.Name);

        }

        [TestMethod]
        public void Approve_Create()
        {
            _userWorkflowController.Approve(1);
            UserWorkflowCreate userWorkflow = (UserWorkflowCreate)_userWorkflowController.Get(1);
            Assert.AreEqual(WorkflowStatus.Approved, userWorkflow.WorkflowStatus);
            Assert.IsNotNull(userWorkflow.Owner);
            Assert.AreEqual(userWorkflow.UserData.Name, userWorkflow.Owner.Name);
            Assert.AreEqual(userWorkflow.UserData.Surname, userWorkflow.Owner.Surname);
            Assert.AreEqual(EntityStatus.None, userWorkflow.Owner.Status);
        }

        [TestMethod]
        public void Approve_Update()
        {
            _userWorkflowController.Approve(1);
            User user = _userService.Get(1);
            user.Name    = "name update";
            user.Surname = "surname update";
            _userService.Save(user);
            user = _userService.Get(1);
            _userWorkflowController.Approve(user.Workflows.Last().Id);


            UserWorkflowUpdate userWorkflow = (UserWorkflowUpdate)_userWorkflowController.Get(user.Workflows.Last().Id);
            Assert.AreEqual(WorkflowStatus.Approved, userWorkflow.WorkflowStatus);
            Assert.IsNotNull(userWorkflow.Owner);
            Assert.AreEqual(userWorkflow.UserData.Name, userWorkflow.Owner.Name);
            Assert.AreEqual("name update",    userWorkflow.Owner.Name);
            Assert.AreEqual("surname update", userWorkflow.Owner.Surname);
            Assert.AreEqual(EntityStatus.None, userWorkflow.Owner.Status);
        }

        [TestMethod]
        public void Approve_Delete()
        {
            //_userWorkflowController.Approve(1);
            //_userService.Delete(1);
            //User user = _userService.Get(1);
            //_userWorkflowController.Approve(user.Workflows.Last().Id);
            //Assert.IsNull(_userService.Get(1));
            //UserWorkflowDelete userWorkflow = (UserWorkflowDelete)_userWorkflowController.Get(1);
            //Assert.AreEqual(WorkflowStatus.Approved, userWorkflow.WorkflowStatus);
            //Assert.IsNull(userWorkflow.Owner);
        }

        [TestMethod]
        public void Reject_Create()
        {
            _userWorkflowController.Reject(1);
            UserWorkflowCreate createWorkflow = (UserWorkflowCreate)_userWorkflowService.Get(1);
            Assert.AreEqual(WorkflowStatus.Rejected, createWorkflow.WorkflowStatus);
            Assert.IsFalse(_userService.List().Any());
        }
        [TestMethod]
        public void Reject_Update()
        {
            _userWorkflowController.Approve(1);
            User user = _userService.Get(1);
            _userService.Save(user);
            _userWorkflowController.Reject(_userWorkflowService.List().Last().Id);
            UserWorkflowUpdate workflow = (UserWorkflowUpdate)_userWorkflowService.List().Last();
            Assert.AreEqual(WorkflowStatus.Rejected, workflow.WorkflowStatus);
            user = _userService.Get(1);
            Assert.IsNotNull(user);
            Assert.AreEqual(EntityStatus.None, user.Status);
        }
        [TestMethod]
        public void Reject_Delete()
        {

        }
    }
}
