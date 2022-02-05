using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]  // create a new user post request
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            // if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            // the "using" statement ensures to call the .despose method 
            // everytime when it's finished, it releases resourse - similar to the context manager
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {

                UserName = registerDto.Username.ToLower(),
                // get bytes for the string input and compute hash
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key // generate a random key
            };

            // use the entity framework to track
            _context.Users.Add(user);
            // actually save the cahnges
            await _context.SaveChangesAsync();

            return user;
        }
        // async task that's returnning a bool value.
        // check if there is an entry already in db
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}