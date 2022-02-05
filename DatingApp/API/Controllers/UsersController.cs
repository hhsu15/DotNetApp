using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //[HttpGet]  // for get request, api/users

        // the basica way
        // public ActionResult<IEnumerable<AppUser>> GetUsers()
        // {
        //     var users = _context.Users.ToList();
        //     return users;
        // }

        // the async way. Use Task to wrap the function, 
        // await the result and use the async version ToListAsync
        [HttpGet]  // for get request, api/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();

        }

        // basic way
        // [HttpGet("{id}")]  // e.g., api/users/3
        // public ActionResult<AppUser> GetUser(int id)
        // {
        //     return _context.Users.Find(id);

        // }

        // async way
        [HttpGet("{id}")]  // e.g., api/users/3
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);

        }
    }
}