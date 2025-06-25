using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using Newtonsoft.Json;

//NyamburaS
// RegisterForm handles user registration input and sends registration data to the API


namespace HospitalManagementSystemClient
{
    public partial class RegisterForm : Form
    {
        //Base url for the USers API endpoints
        private const string apiBaseUrl = "http://localhost:5277/api/users"; 
        public RegisterForm()
        {
            InitializeComponent();

            //Bind the ComboBox to the Role Enum (Doctor, Nurse etc..) 
            comboBox_role.DataSource = Enum.GetNames(typeof(Role));
            comboBox_role.DropDownStyle = ComboBoxStyle.DropDownList; // restrict typing

        }

        //Register Button CLick Event
        private async void btn_register_Click(object sender, EventArgs e)
        {
            //Gathers and trims input fields 
            var username = txb_username.Text.Trim();
            var password = txb_password.Text;
            var fullName = txb_fullname.Text;
            var email = txb_email.Text;
            var phone = txb_phone.Text;
            var address = txb_address.Text;
            var roleStr = comboBox_role.SelectedItem?.ToString();

            //Field Validation: Prevents registration with any missing or blank fields
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrEmpty(roleStr))
            {
                MessageBox.Show("Please fill in all fields before registering.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

            //convert the selected item string back to the Role enum
            Role selectedRole = (Role)Enum.Parse(typeof(Role), roleStr);

            // Builds the registration data object to send to API
            var registrationDto = new
            {
                Username = txb_username.Text.Trim(),
                Password = txb_password.Text,
                Email = txb_email.Text.Trim(),
                Roles = new List<string> { selectedRole.ToString() }, // Sends roles as string list for API parsing
                Profile = new
                {
                    FullName = txb_fullname.Text,
                    Phone = txb_phone.Text,
                    Address = txb_address.Text
                }
            };

            // Serializes the registration data to JSON string
            string json = JsonConvert.SerializeObject(registrationDto); // serialization

            using (var client = new HttpClient())
            {
                // Creates HTTP content with JSON payload and proper content-type header
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    // This Sends POST request asynchronously to the API register endpoint
                    var response = await client.PostAsync($"{apiBaseUrl}/register", content);
                    // If registration succeeds (HTTP 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Closes the register form after success
                    }
                    else
                    {
                        // If registration fails, read and display error message from API response
                        string error = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show("Registration failed: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Shows any network or unexpected exceptions and notify the user
                    MessageBox.Show("Error: " + ex.Message.ToString());
                }

            }

            this.Close(); //closes registration form
        }
       

    }
    
}
