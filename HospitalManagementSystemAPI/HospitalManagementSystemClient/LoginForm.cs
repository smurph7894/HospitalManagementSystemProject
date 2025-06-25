using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using Newtonsoft.Json; // For reading app.config

//Nyamburas 
namespace HospitalManagementSystemClient
{ 

    public partial class LoginForm : Form
    {
        // Base URL for user API endpoints
        private const string apiBaseUrl = "http://localhost:5277/api/users";
        public LoginForm()
        {
            InitializeComponent();

        }

        //login button click event
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            //reads and trims user input
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            //validation that both username and password have been entered 
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            //validating credentials, Checks if user exists and password matches
            try
            {
                // Attempts authentication with API
                var loginSuccess = await AuthenticateUser(username, password);

                if (loginSuccess != null)
                {
                    // This will map the response data to the client-side Users model
                    var user = MapUserResponseToUsers(loginSuccess); 

                    MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Opens the Dashboard form, passing in the authenticated user
                    var dashboard = new DashBoardForm(user);  
                    dashboard.Show();

                    // Hides login form after successful login
                    this.Hide();
                }
                else
                {
                    // error shown if login fails (wrong credentials)
                    MessageBox.Show("Invalid credentials. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // eror shown if network or unexpected errors
                MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Sends login request to API with provided username and password
        private async Task<UserResponse> AuthenticateUser(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                var loginPayload = new
                {
                    Username = username,
                    Password = password
                };
                // This will serialize login credentials to JSON
                string json = JsonConvert.SerializeObject(loginPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // POST to login endpoint
                var response = await client.PostAsync($"{apiBaseUrl}/login", content);

                // On success, read and deserialize response body
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserResponse>(body);
                }
            }
            return null; // Returns null if login failed
        }

        //navigating to registration form
        private void btn_register_Click(object sender, EventArgs e)
        {
            this.Hide(); //hide the loginform temporarily 

            //Creates a new object from the class called registerForm and name the variable that holds it RegisterForm.
            RegisterForm registerForm = new RegisterForm();

            //shows the form object stored in the variable registerForm
            registerForm.ShowDialog();

            this.Show(); // Bring back login form when registration closes

        }

        // Maps API UserResponse DTO to local Users model with enums and profile data
        private Users MapUserResponseToUsers(UserResponse res)
        {
            return new Users
            {
                Username = res.Username,
                Email = res.Email,
                Roles = res.Roles
                    .Select(r => Enum.TryParse<Role>(r, out var role) ? role : Role.Patient)
                    .ToList(),

                Permissions = res.Permissions
                  .Select(p => Enum.TryParse<Permission>(p, out var perm) ? perm : Permission.None)
                   .ToList(),

                Profile = new Profile
                {
                    FullName = res.Profile.FullName,
                    Phone = res.Profile.Phone,
                    Address = res.Profile.Address
                },

                UserId = res.UserId
            };
        }


    }

    // DTO for deserializing login response from API
    internal class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }        // still string from API
        public List<string> Permissions { get; set; }  // also string from API
        public Profile Profile { get; set; }
        public string UserId { get; set; }
    }

    // Users model with enums and profile for client side use
    public class Users
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
        public List<Permission> Permissions { get; set; }
        public Profile Profile { get; set; }
        public string UserId { get; set; }
    }

    // Role enum defines user roles in the system
    public enum Role
    {
        Patient,
        Staff,
        Nurse,
        Doctor,
        Admin
    }

    // Permission enum defines system capabilities for users
    public enum Permission
    {
        None,
        ScheduleApp,
        ManageInventory,
        AccessReports,
        ManageStaff,
        ViewVitals,
        ManageAdmissions,
        ManageAppointments,
        ManageBeds,
        ManageDepartments,
        ManageUsers
    }

}
