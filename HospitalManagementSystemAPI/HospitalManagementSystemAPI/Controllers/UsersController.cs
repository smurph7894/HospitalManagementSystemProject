using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // Entity Framework database context for SQL Server
        private readonly AppDbContext _context;

        // Constructor sets up MongoDB collection and injects EF DbContext for SQL operations
        public UsersController(IMongoClient mongoClient, AppDbContext context)
        {
            // Use injected Mongo client to access the HospitalManagementDB and 'userData' collection
            var database = mongoClient.GetDatabase("HospitalManagementDB");

            // Use correct collection name matching your DB (example: mine is "userData")
            _userCollection = database.GetCollection<Users>("userData");
            _context = context; // EF DbContext for SQL DB
        }


        // GET api/users/check-username?username=foo
        // Checks if a username already exists in MongoDB to prevent duplicate registrations
        [HttpGet("check-username")]
        public async Task<IActionResult> UsernameExists([FromQuery] string username)
        {
            var exists = await _userCollection.Find(u => u.Username == username).AnyAsync();
            return Ok(exists);
        }

        // POST api/users/login
        // Authenticates user by username and password; returns user info if valid or 401 Unauthorized if invalid
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

        // POST api/users/register
        // Registers a new user, inserting into MongoDB, then creates matching SQL records (Staff or Patient) accordingly
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            // Check if username is already taken
            var exists = await _userCollection.Find(u => u.Username == dto.Username).AnyAsync();
            if (exists)
                return BadRequest("Username already exists.");

            // Parse string roles to enum; default to Patient if parsing fails
            var rolesEnum = dto.Roles.Select(r => Enum.TryParse<Role>(r, out var parsed) ? parsed : Role.Patient).ToList();

            // Generate permissions based on roles
            var permissions = PermissionHelper.GetPermissionsForRoles(rolesEnum);

            // Create MongoDB user document
            var newUser = new Users
            {
                UserId = ObjectId.GenerateNewId(),
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                Roles = rolesEnum,
                Permissions = permissions,
                Profile = dto.Profile, //Includes FullName, Phone, Address, DOB, Gender
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Insert new user document in MongoDB
            await _userCollection.InsertOneAsync(newUser);

            try
            {
                // For staff roles, check or create default Department and insert Staff record in SQL DB
                if (rolesEnum.Any(r => r == Role.Doctor ||r == Role.Nurse ||r == Role.AdministrativeStaff ||r == Role.Staff))
                {
                    int departmentId;
                    var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentId == 1);
                    if (!departmentExists)
                    {
                        // Create default department if not present
                        var defaultDept = new Department { Name = "Default", Description = "Default department" };
                        _context.Departments.Add(defaultDept);
                        await _context.SaveChangesAsync();
                        departmentId = defaultDept.DepartmentId;
                    }
                    else
                    {
                        departmentId = 1; //// Use default department id
                    }

                    // Create Staff SQL entity linked to Mongo user ID
                    var staff = new Staff
                    {
                        UserRef = newUser.UserId.ToString(),
                        Name = dto.Profile.FullName,
                        StaffType = "General",
                        DepartmentId = departmentId,
                        Phone = dto.Profile.Phone,
                        Email = dto.Email,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Staff.Add(staff);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        var sqlException = ex.InnerException;
                        // Log or inspect sqlException.Message or ex.Message to get details
                        return StatusCode(500, "Database update error: " + sqlException?.Message ?? ex.Message);
                    }

                }
                else if (rolesEnum.Count == 1 && rolesEnum[0] == Role.Patient)
                {
                    // For patient role only, insert Patient record in SQL DB
                    var patient = new Patient
                    {
                        PatientOrgId = newUser.UserId.ToString(),
                        FirstName = dto.Profile.FullName.Split(' ')[0],
                        LastName = dto.Profile.FullName.Split(' ').Length > 1 ? dto.Profile.FullName.Split(' ')[1] : "",
                        Email = dto.Email,
                        Phone = dto.Profile.Phone,
                        Address = dto.Profile.Address,
                        Gender = dto.Profile.Gender, // Assign a valid char here, e.g., 'M', 'F', 'O'
                        DOB = DateTime.UtcNow.AddYears(-25), // Example DOB, adjust or get from DTO
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync(); // Save any pending changes
                }

                await _context.SaveChangesAsync(); // async save changes!
            }
            catch (Exception ex)
            {
                // Handle failure in SQL insertion; might consider rolling back Mongo insert for later time
                return StatusCode(500, "MongoDB registration succeeded but SQL insertion failed: " + ex.Message);
            }

            return Ok("User registered.");
        }

        // GET api/users/find?username=foo
        // Finds a user document by username in MongoDB
        [HttpGet("find")]
        public async Task<IActionResult> FindUser([FromQuery] string username)
        {
            var user = await _userCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }

        // DELETE api/users/{id}
        // Deletes a user document from MongoDB by ObjectId string
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
