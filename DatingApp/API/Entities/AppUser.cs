using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        // create id property. Id is a convension and the framework knows this
        // so it will be treated as primary key 
        public int Id { get; set; }
        public string UserName { get; set; }

        // properties for password and salt
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}