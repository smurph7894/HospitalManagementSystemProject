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
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        string category = "category"; // Default category if none selected
        private Patient selectedPatient;

        public PatientSearchForm(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;
            LoadPatientsList();
        }
        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    var response = await client.GetAsync($"{apiBaseUrl}/patient/all");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                    dataGridView_PatientList.DataSource = patients;
                    dataGridView_PatientList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView_PatientList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

        private async void btn_search_Click(object sender, EventArgs e)
        {
            try
            {                
                using (HttpClient client = new HttpClient())
                {
                    string searchInput = txtB_searchText.Text.Trim();
                    if (string.IsNullOrEmpty(searchInput))
                    {
                        MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var response = client.GetAsync($"{apiBaseUrl}/patient/search/{category}/{searchInput}").Result;
                    response.EnsureSuccessStatusCode();
                    var json = response.Content.ReadAsStringAsync().Result;
                    var patients = JsonConvert.DeserializeObject<List<Patient>>(json);

                    if (patients == null || !patients.Any())
                    {
                        MessageBox.Show("No patients found matching the search criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_PatientList.DataSource = null; 
                    }
                    else
                    {
                        dataGridView_PatientList.DataSource = patients;
                    }
                }
                comboBox_searchCategories.SelectedIndex = 0; // Reset to default category
                txtB_searchText.Clear(); // Clear the search text box
            }
            catch (Exception ex)
            {
                comboBox_searchCategories.SelectedIndex = 0; // Reset to default category
                txtB_searchText.Clear(); // Clear the search text box
                MessageBox.Show("An error occurred while searching for patients. Please try a new search. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Search error: {ex.Message}");
            }
        }

        private void selected_category_Changed(object sender, EventArgs e)
        {
            if (comboBox_searchCategories.SelectedIndex == 0)
            {
                category = "category"; // Default category if none selected
            }
            else
            {
                category = comboBox_searchCategories.SelectedItem.ToString();
            }
        }

        private async void btn_deletePatient_Click(object sender, EventArgs e)
        {
            var patientIdMongo = selectedPatient.PatientOrgId;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (selectedPatient == null)
                    {
                        MessageBox.Show("Please select a patient to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var response = client.DeleteAsync($"{apiBaseUrl}/patient/{selectedPatient.PatientId}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseMongo = client.DeleteAsync($"{apiBaseUrl}/users/{patientIdMongo}").Result;
                        if (responseMongo.IsSuccessStatusCode)
                        {
                            MessageBox.Show("User account deleted successfully .", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPatientsList(); // Refresh the list after deletion
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete user account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete patient. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("errrorMessage:", errorMessage);
                MessageBox.Show("An error occurred while deleting the patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Delete error: {ex.Message}");
            }
        }

        private void btn_getAllPatients_Click(object sender, EventArgs e)
        {
            LoadPatientsList();
        }

        private void dataGridView_PatientList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedPatient = dataGridView_PatientList.Rows[e.RowIndex].DataBoundItem as Patient;
        }

        private void btn_editPatientInfo_Click(object sender, EventArgs e)
        {
            this.Close();
            // Pass the full Users object to the Dashboard form (or Form1)
            var patientUserInfo = new PatientUserInfo(_loggedInUser, selectedPatient);
            patientUserInfo.Show();
        }

        private void btn_viewPatientHistory_Click(object sender, EventArgs e)
        {
            if(selectedPatient == null)
            {
                MessageBox.Show("Please select a patient to view their medical history.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.Close();
                // Pass the full Users object to the Dashboard form (or Form1)
                var medicalHistoryForm = new MedicalHistoryForm(_loggedInUser, selectedPatient);
                medicalHistoryForm.Show();
            }
        }
    }
}
