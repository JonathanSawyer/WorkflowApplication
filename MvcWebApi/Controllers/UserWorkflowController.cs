﻿using BL;
using BL.Workflow;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcWebApi.Controllers
{
    //TODO: Generic Workflow Controller
    public class UserWorkflowController : ApiController
    {
        IEntityService<BL.User>     _userService;
        IWorkflowService<BL.User>   _workflowService;

        public UserWorkflowController(IEntityService<BL.User> userService, 
                                      IWorkflowService<BL.User> workflowService)
        {
            _workflowService = workflowService;
            _userService     = userService;
        }

        // GET api/userworkflow
        public dynamic Get()
        {
            IList<BL.Workflow.EntityWorkflow<User>> workflows = _workflowService.List();
            return from x in workflows
                   let userData = (IUserData)x
                   select new
                   {
                       Name     = userData.UserData.Name,
                       Surname  = userData.UserData.Surname,
                       Type     = Enum.GetName(typeof(WorkflowType), x.WorkflowType),
                       Status   = Enum.GetName(typeof(WorkflowStatus), x.WorkflowStatus)
                   };
                   
        }

        // GET api/userworkflow/5
        public BL.Workflow.EntityWorkflow<User> Get(int id)
        {
            return _workflowService.Get(id);
        }

        [HttpGet]
        //[Route("api/userworkflow/approve")]
        public void Approve(int id)
        {
            _workflowService.Approve(id);
        }

        [HttpGet]
        //[Route("api/userworkflow/approve")]
        public void Reject(int id)
        {
            _workflowService.Reject(id);
        }

    }
}