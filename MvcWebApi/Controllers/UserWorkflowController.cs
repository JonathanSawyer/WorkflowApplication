using BL;
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
        
        // GET api/user
        public IList<BL.Workflow.EntityWorkflow<User>> Get()
        {
            return _workflowService.List();
        }

        // GET api/user/5
        public BL.Workflow.EntityWorkflow<User> Get(int id)
        {
            return _workflowService.Get(id);
        }

        [HttpGet]
        //[Route("api/workflow/approve")]
        public void Approve(int id)
        {
            _workflowService.Approve(id);
        }

        [HttpGet]
        //[Route("api/workflow/approve")]
        public void Reject(int id)
        {
            _workflowService.Reject(id);
        }

    }
}