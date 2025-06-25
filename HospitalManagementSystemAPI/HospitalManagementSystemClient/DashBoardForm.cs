using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using Newtonsoft.Json;

//Nyamburas
namespace HospitalManagementSystemClient
{
    public partial class DashBoardForm : Form
    {
        // Stores the currently logged-in user object with roles and permissions
        private Users _loggedInUser;
        private List<string> userPermissions;
        private Patient _patient;
        private readonly string apiBaseUrl = "http://localhost:5277/api";

        // Constructor receives the logged-in user and initializes the dashboard
        public DashBoardForm(Users user)
        {
            InitializeComponent();

            _loggedInUser = user;
            SetupDashboard();
        }

        // Sets button visibility based on the user's permissions
        private void SetupDashboard()
        {
            //check for null user or permissions
            if (_loggedInUser == null || _loggedInUser.Permissions == null)
            {
                MessageBox.Show("User permissions not loaded. Please contact admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Converts enum permissions to string list for easier comparison
            var permissions = _loggedInUser.Permissions.Select(p => p.ToString()).ToList();
            // Show or hide buttons based on permissions
            btn_ScheduleAppointments.Visible = permissions.Contains("ScheduleApp");
            btn_ManageInventory.Visible = permissions.Contains("ManageInventory");
            btn_AccessReports.Visible = permissions.Contains("AccessReports");
            btn_ManageStaff.Visible = permissions.Contains("ManageStaff");
            btn_ViewVitals.Visible = permissions.Contains("ViewVitals");
            btn_ManageAdmissions.Visible = permissions.Contains("ManageAdmissions");
            btn_ManageAppointments.Visible = permissions.Contains("ManageAppointments");
            btn_ManageBeds.Visible = permissions.Contains("ManageBeds");
            btn_ManageDepartments.Visible = permissions.Contains("ManageDepartments");
            btn_ManageUsers.Visible = permissions.Contains("ManageUsers");
            btn_patientSearch.Visible = permissions.Contains("ViewVitals");
            btn_myInfo.Visible = _loggedInUser.Roles.Contains(Role.Patient); //staff with access can access myInfo through patientSearch - Sarah
            /* MedicalHistoryForm permission is controlled by how the user access the page
            *    if they are a patient, they can view their own history, if
            *    their permission allows medical history access, the selected patient info will pass through
            *    -Sarah
            */

            // Chat button is always visible 
            btn_OpenChat.Visible = true;
        }

        // Opens the SignalR chat form, passing the logged-in user for context
        private void btn_OpenChat_Click(object sender, EventArgs e)
        {
            var chatForm = new Form1(_loggedInUser);
            chatForm.Show();


        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {

        }

        // Opens the Inventory Management form when the corresponding button is clicked
        private void btn_ManageInventory_Click(object sender, EventArgs e)
        {
            var form = new InventoryManagementForm(_loggedInUser);
            form.Show();

        }

        private void btn_ScheduleAppointments_Click(object sender, EventArgs e)
        {
            var form = new ScheduleAppointmentsForm(_loggedInUser);
            form.Show();

        }

        private async void btn_myInfo_Click(object sender, EventArgs e)
        {
            await GetpatientInfo();//get the patient info for the logged in user

            this.Close();
            var form = new PatientUserInfo(_loggedInUser, _patient); //putting in twice so a patient can view their own info
            form.Show();
        }

        private void btn_patientSearch_Click(object sender, EventArgs e)
        {
            this.Close();
            var form = new PatientSearchForm(_loggedInUser);
            form.Show();
        }


        private async Task GetpatientInfo()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/patient/userId/{_loggedInUser.UserId}");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    _patient = JsonConvert.DeserializeObject<Patient>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving patient information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_SignOut_Click(object sender, EventArgs e)
        {
            // Show the login form
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // Closes the inventory form
            this.Close();

        }
    }
}
