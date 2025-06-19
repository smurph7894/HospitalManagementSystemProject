using System;
using System.Windows.Forms;
using HospitalManagementSystemClient.Services; // For reading app.config

//Nyamburas 
namespace HospitalManagementSystemClient
{ 

    public partial class LoginForm : Form
    {
       private MongoDbService _mongoDbService;
        public LoginForm()
        {
            InitializeComponent();

            //Initializes mongoDB service connecting 'userData' collection
            _mongoDbService = new MongoDbService("HospitalManagementDB", "userData");
        }

        //login button click event
        private void btnLogin_Click(object sender, EventArgs e)
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

            //attempting to fetch user by their username
            var user = _mongoDbService.FindUserByUsername(username);

            //validating credentials, Checks if user exists and password matches
            if (user != null && user.Password == password)
            {
                MessageBox.Show($"Welcome {user.Profile.FullName}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //Declare loggedInUser and pass to dashboard
                var loggedInUser = user;

                this.Hide();

                // Pass the full Users object to the Dashboard form (or Form1)
                var dashBoardForm = new DashBoardForm(user);
                dashBoardForm.Show();
            }
            else
            { 
                //error is shown if authentication fails
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Hide(); //hide the loginform temporarily 

            
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

      
    }
}
