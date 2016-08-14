using BL;
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
    //TODO: Chanehe return types to enumerable
    //Generic Entity Controller
    public class UserController : ApiController
    {
        IEntityService<BL.User> _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        // GET api/user
        public List<dynamic> Get()
        {
            IList<BL.User> users = _userService.List();
            return users.Select(
                x => new
                {
                    Id   = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Status = Enum.GetName(typeof(EntityStatus), x.Status),
                    Workflows = from y in x.Workflows
                                let userData = (IUserData)y
                                select new
                                {
                                    Id = y.Id,
                                    Name = userData.UserData.Name,
                                    Surname = userData.UserData.Surname,
                                    Type = Enum.GetName(typeof(WorkflowType), y.WorkflowType),
                                    Status = Enum.GetName(typeof(WorkflowStatus), y.WorkflowStatus)
                                }
                }).ToList<dynamic>();
        }

        // GET api/user/5
        public dynamic Get(int id)
        {
            User user =_userService.Get(id);
            return new
            {
                Name = user.Name,
                Surname = user.Surname,
                Status = Enum.GetName(typeof(EntityStatus), user.Status)
            };
        }

        // POST api/user
        public void Post([FromBody]BL.User user)
        {
            _userService.Save(user);
        }

        // DELETE api/values/5
        public UserServiceResult Delete(int id)
        {
            return _userService.Delete(id);
        }
    }
}