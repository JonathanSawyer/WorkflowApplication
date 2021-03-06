﻿using IdemWokflow.Bll;
using IdemWokflow.Bll.Workflow;
using IdemWokflow.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdemWokflow.Web.Controllers
{
    //TODO: Generic Workflow Controller
    public class UserWorkflowController : ApiController
    {
        IEntityService<User>     _userService;
        IWorkflowService<User>   _workflowService;

        public UserWorkflowController(IEntityService<User> userService, 
                                      IWorkflowService<User> workflowService)
        {
            _workflowService = workflowService;
            _userService     = userService;
        }

        // GET api/userworkflow
        public IEnumerable<object> Get()
        {
            IList<EntityWorkflow<User>> workflows = _workflowService.List();
            return from x in workflows
                   let userData = (IUserData)x
                   select new
                   {
                       Id               = x.Id,
                       Name             = userData.UserData.Name,
                       Surname          = userData.UserData.Surname,
                       Type             = Enum.GetName(typeof(WorkflowType), x.WorkflowType),
                       Status           = Enum.GetName(typeof(WorkflowStatus), x.WorkflowStatus),
                       MakerDateTime    = x.MakerDateTime,
                       ApproverDateTime = x.ApproverDateTime
                   };
        }

        // GET api/userworkflow/5
        public EntityWorkflow<User> Get(int id)
        {
            return _workflowService.Get(id);
        }

        [HttpGet]
        [Route("api/userworkflow/approve/{workflowId}")]
        public void Approve(int workflowId)
        {
            _workflowService.Approve(workflowId);
        }

        [HttpGet]
        [Route("api/userworkflow/reject/{workflowId}")]
        public void Reject(int workflowId)
        {
            _workflowService.Reject(workflowId);
        }

    }
}