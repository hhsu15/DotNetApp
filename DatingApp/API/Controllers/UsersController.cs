using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Give the controller some attributes
    [ApiController]
    [Route("api/[controller]")]  // in order to use this controller, client will have to specify the rounte this way, for example api/users (with the Controller parrt)
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]  // for get request, api/users
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        [HttpGet("{id}")]  // e.g., api/users/3
        public ActionResult<AppUser> GetUser(int id)
        {
            return _context.Users.Find(id);

        }
    }
}