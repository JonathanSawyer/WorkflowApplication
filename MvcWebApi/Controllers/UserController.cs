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
                    Name = x.Name,
                    Surname = x.Surname,
                    Status = Enum.GetName(typeof(EntityStatus), x.Status)
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