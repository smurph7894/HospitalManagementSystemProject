using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HospitalManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController: ControllerBase
    {
        private readonly IMongoCollection<Users> _userCollection;

        public UsersController(IMongoClient client)
        {
            var database = client.GetDatabase("HospitalManagementDB");
            _userCollection = database.GetCollection<Users>("Users");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            // Check if username exists
            var existingUser = await _userCollection.Find(u => u.Username == dto.Username).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            // Map DTO to User model
            var newUser = new Users
            {
                Username = dto.Username,
                Password = dto.Password, // TODO: Hash password before saving
                Email = dto.Email,
                Roles = dto.Roles,
                Permissions = PermissionHelper.GetPermissionsForRoles(dto.Roles),
                Profile = dto.Profile,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userCollection.InsertOneAsync(newUser);

            return Ok("User registered successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var objectId = ObjectId.Parse(id);
            var result = await _userCollection.DeleteOneAsync(u => u.UserId == objectId);
            Users find = await _userCollection.Find(u => u.UserId == objectId).FirstOrDefaultAsync(); //if check on delete being finicky, using a find to confirm instead so it's accurate 100% of time  - Sarah
            if (find == null)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");
        }
    }

    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
        public Profile Profile { get; set; }
    }
}
