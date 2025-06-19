using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
            var database = client.GetDatabase("HospitalDB");
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
