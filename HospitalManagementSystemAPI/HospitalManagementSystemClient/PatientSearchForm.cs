using HospitalManagementSystemClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using HospitalManagementSystemClient.Models;
using Newtonsoft.Json;


namespace HospitalManagementSystemClient
{
    public partial class PatientSearchForm : Form
    {
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "https://localhost:5277/api/Patient";
        public PatientSearchForm(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;
            LoadPatientsList();
            //InitializeGrid();
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Pass the full Users object to the Dashboard form (or Form1)
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        private async void LoadPatientsList()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiBaseUrl + "/all");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                    dataGridView_PatientList.DataSource = patients;
                }
            }
            catch (HttpRequestException error)
            {
                Console.WriteLine($"Request error: {error.Message}");
                if(error.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {error.InnerException.Message}");
                }
                MessageBox.Show("An error occurred while loading patients: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeGrid()
        {
            
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void btn_editPatientInfo_Click(object sender, EventArgs e)
        {

        }

        private void btn_deletePatient_Click(object sender, EventArgs e)
        {

        }
    }
}
