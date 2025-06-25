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
        // MongoDB collection to store Users documents
        private readonly IMongoCollection<Users> _userCollection;

        // Constructor initializes MongoDB connection and selects database/collection
        public UsersController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("HospitalManagementDB");

            // Use correct collection name matching your DB (example: mine is "userData")
            _userCollection = database.GetCollection<Users>("userData"); 
        }


        // Endpoint to check if a username already exists in DB (GET /api/users/check-username?username=foo)
        [HttpGet("check-username")]
        public async Task<IActionResult> UsernameExists([FromQuery] string username)
        {
            var exists = await _userCollection.Find(u => u.Username == username).AnyAsync();
            return Ok(exists);
        }

        // Endpoint for user login (POST /api/users/login)
        // Receives username and password, returns user info if valid or 401 Unauthorized
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userCollection.Find(u =>
       u.Username == dto.Username && u.Password == dto.Password).FirstOrDefaultAsync();

            if (user == null)
                return Unauthorized("Invalid username or password.");

            // Build a clean object (no Mongo-specific types)
            var userResponse = new
            {
                UserId = user.UserId.ToString(), // serialize ObjectId as string
                Username = user.Username,
                Email = user.Email,
                Roles = user.Roles.Select(r => r.ToString()).ToList(),
                Permissions = user.Permissions.Select(p => p.ToString()).ToList(),
                Profile = new
                {
                    FullName = user.Profile.FullName,
                    Phone = user.Profile.Phone,
                    Address = user.Profile.Address
                }
            };

            return Ok(userResponse);
        }

        // Endpoint for user registration (POST /api/users/register)
        // Accepts RegisterUserDto with username, password, email, roles as strings, and profile
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            // Checks if username already exists in the database
            var exists = await _userCollection.Find(u => u.Username == dto.Username).AnyAsync();
            if (exists)
                return BadRequest("Username already exists.");

            // Converts string roles from client to Role enum list
            var rolesEnum = dto.Roles
                .Select(r => Enum.TryParse<Role>(r, out var parsed) ? parsed : Role.Patient)
                .ToList();

            // Gets permissions based on roles using helper class
            var permissions = PermissionHelper.GetPermissionsForRoles(rolesEnum);

            // Creates new Users document with generated ObjectId and current timestamps
            var newUser = new Users
            {
                UserId = ObjectId.GenerateNewId(),
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                Roles = rolesEnum,
                Permissions = permissions,
                Profile = dto.Profile,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Inserts new user into the MongoDB collection
            await _userCollection.InsertOneAsync(newUser);

            //success message returned
            return Ok("User registered.");
        }

        // Endpoint to find a user by username (GET /api/users/find?username=foo)
        [HttpGet("find")]
        public async Task<IActionResult> FindUser([FromQuery] string username)
        {
            var user = await _userCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Endpoint to delete a user by ObjectId string (DELETE /api/users/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var objectId = ObjectId.Parse(id);

            // Deletes user document matching ObjectId
            var result = await _userCollection.DeleteOneAsync(u => u.UserId == objectId);

            // Confirms deletion by trying to find user after delete
            Users find = await _userCollection.Find(u => u.UserId == objectId).FirstOrDefaultAsync(); //if check on delete being finicky, using a find to confirm instead so it's accurate 100% of time  - Sarah | Thank you -Sally
            if (find == null)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");
        }
    }

    // DTO for user login request payload
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    // DTO for user registration payload
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public Profile Profile { get; set; }
    }
}
