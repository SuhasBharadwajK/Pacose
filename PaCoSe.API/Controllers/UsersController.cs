using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaCoSe.Models;

namespace PaCoSe.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        // POST /add -> User Token
        [HttpPut("")]
        public User AddUser(User user)
        {
            return null;
        }

        // GET / -> User Token
        [HttpGet("")]
        public List<User> GetAllUsers()
        {
            return null;
        }

        // GET /:id -> User Token
        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            return null;
        }

        // DELETE /:id -> User Token
        [HttpDelete("{id}")]
        public bool DeleteUser(int id)
        {
            return false; // Not needed
        }

        // PUT /:id -> User Token
        [HttpPut("{id}")]
        public User UpdateUser(int id, User user)
        {
            return null;
        }

        // PUT /disable/:id -> User Token
        [HttpPut("{id}")]
        public bool DisableUser(int id)
        {
            return false; // Not needed
        }
    }
}
