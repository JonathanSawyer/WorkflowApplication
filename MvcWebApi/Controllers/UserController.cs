using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcWebApi.Controllers
{
    public class UserController : ApiController
    {
        IEntityService<BL.User> _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        // GET api/user
        public IList<BL.User> Get()
        {
            return _userService.List();
        }

        // GET api/user/5
        public BL.User Get(int id)
        {
            return _userService.Get(id);
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