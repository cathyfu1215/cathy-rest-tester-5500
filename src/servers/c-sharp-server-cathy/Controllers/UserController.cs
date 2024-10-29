using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UserApi
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpDelete]
        public IActionResult DeleteAllUsers()
        {
            users.Clear();
            nextId = 1;
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Name))
            {
                return BadRequest("Name is required and must be a non-empty string");
            }
            newUser.Id = nextId++;
            newUser.HoursWorked = 0;
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUserById), new { userId = newUser.Id }, newUser);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (!string.IsNullOrWhiteSpace(updatedUser.Name))
            {
                user.Name = updatedUser.Name.Trim();
            }
            return Ok(user);
        }

        [HttpPatch("{userId}")]
        public IActionResult UpdateUserHours(int userId, [FromBody] HoursUpdate hoursUpdate)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (hoursUpdate.HoursToAdd < 0)
            {
                return BadRequest("Invalid hoursToAdd value");
            }
            user.HoursWorked += hoursUpdate.HoursToAdd;
            return Ok(user);
        }

        public class HoursUpdate
        {
            public int HoursToAdd { get; set; }
        }

            

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            users.Remove(user);
            return Ok(user);
        }
    }
}
