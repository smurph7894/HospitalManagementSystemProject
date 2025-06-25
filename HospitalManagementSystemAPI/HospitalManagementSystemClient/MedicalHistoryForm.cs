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
    public partial class MedicalHistoryForm : Form
    {
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        private Patient selectedPatient;
        private Appointment selectedAppointment;
        private Admission selectedAdmission;
        private CarePlan selectedCarePlan;

        private readonly HttpClient client = new HttpClient();

        public MedicalHistoryForm(Users loggedInUser, Patient patientInfo)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            selectedPatient = patientInfo;

            //populate titles
            lbl_patientId.Text = selectedPatient.PatientId.ToString();
            lbl_patientName.Text = $"{selectedPatient.FirstName} {selectedPatient.LastName}";
            LoadMedicalHistory();
        }

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            // Navigate to the dashboard form 
            this.Close();
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            // If the logged-in user is the patient, go back to their info
            if (_loggedInUser.Roles.Contains(Role.Patient))
            {
                this.Close();
                var patientForm = new PatientUserInfo(_loggedInUser, selectedPatient);
                patientForm.Show();
            }
            //if the logged-in user is not the patient, go back to the patient search form
            else
            {
                this.Close();
                var patientSearchForm = new PatientSearchForm(_loggedInUser);
                patientSearchForm.Show();
            }
        }

        private async void LoadMedicalHistory()
        {
            //get the top grids medical history of the selected patient

            //Admissions
            var admissionResponse = await client.GetAsync($"{apiBaseUrl}/admission/patient/{selectedPatient.PatientId}");
            var admissions = JsonConvert.DeserializeObject<List<Admission>>(await admissionResponse.Content.ReadAsStringAsync());
            if (admissionResponse.IsSuccessStatusCode)
            {
                dgv_admissions.DataSource = admissions;
                dgv_admissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_admissions.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Failed to retrieve admissions.");
            }

            //Appointments
            var appointmentResponse = await client.GetAsync($"{apiBaseUrl}/appointment/patient/{selectedPatient.PatientId}");
            var appointmentJson = await appointmentResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(appointmentJson))
            {
                MessageBox.Show("No appointments available for this patient.");
                return;
            }
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentJson);
            if (appointmentResponse.IsSuccessStatusCode)
            {
                dgv_appointments.DataSource = appointments;
                dgv_appointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_appointments.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Failed to retrieve appointments.");
            }

            //CarePlans
            var carePlanResponse = await client.GetAsync($"{apiBaseUrl}/careplan/patient/{selectedPatient.PatientId}");
            var carePlanJson = await carePlanResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(carePlanJson))
            {
                MessageBox.Show("No care plans available for this patient.");
                return;
            }
            var carePlans = JsonConvert.DeserializeObject<List<CarePlan>>(carePlanJson);
            if (carePlanResponse.IsSuccessStatusCode)
            {
                dgv_carePlans.DataSource = carePlans;
                dgv_carePlans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_carePlans.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Failed to retrieve careplans.");
            }

            //Vitals
            var vitalsResponse = await client.GetAsync($"{apiBaseUrl}/vitals/patient/{selectedPatient.PatientId}");
            var vitalsJson = await vitalsResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(vitalsJson))
            {
                MessageBox.Show("No vitals data available for this patient.");
                return;
            }
            var vitals = JsonConvert.DeserializeObject<List<Vitals>>(vitalsJson);
            if (vitalsResponse.IsSuccessStatusCode)
            {
                dgv_vitals.DataSource = vitals;
                dgv_vitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_vitals.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Failed to retrieve vitals.");
            }
        }

        private void dgv_appointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgv_appointments.Rows[e.RowIndex];
            selectedAppointment = selectedRow.DataBoundItem as Appointment;
            if (selectedAppointment == null)
            {
                MessageBox.Show("No appointment selected. Please select an appointment to view details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // If an appointment is selected, retrieve and display its vitals
                getAppointmentCarePlansAndUpdates(selectedAppointment);
            }
        }

        private void dgv_admissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgv_admissions.Rows[e.RowIndex];
            selectedAdmission = selectedRow.DataBoundItem as Admission;
            if (selectedAdmission == null)
            {
                MessageBox.Show("No admission selected. Please select an admission to view details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // If an admission is selected, retrieve and display its bed details
                getAdmissionBeds(selectedAdmission.BedId);
            }
        }

        private void dgv_carePlans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgv_carePlans.Rows[e.RowIndex];
            selectedCarePlan = selectedRow.DataBoundItem as CarePlan;
            if (selectedCarePlan == null)
            {
                MessageBox.Show("No care plan selected. Please select a care plan to view details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // If a care plan is selected, retrieve and display its updates
                getCarePlanUpdatesByCarePlan(selectedCarePlan.CarePlanId);
            }
        }

        private async void getAdmissionBeds(int bedId)
        {
            dgv_beds.DataSource = null; // Clear previous data

            var bedResponse = await client.GetAsync($"{apiBaseUrl}/admission/bed/{bedId}");
            var bedJson = await bedResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(bedJson))
            {
                MessageBox.Show("No bed details available for this admission.");
                return;
            }
            var bedDetails = JsonConvert.DeserializeObject<Bed>(bedJson);
            if (bedResponse.IsSuccessStatusCode)
            {
                dgv_beds.DataSource = new List<Bed> { bedDetails };
                dgv_beds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_beds.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Failed to retrieve bed details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void getCarePlanUpdatesByCarePlan(int carePlanId)
        {
            dgv_carePlanUpdates.DataSource = null; // Clear previous data

            var carePlanUpdatesResponse = await client.GetAsync($"{apiBaseUrl}/careplan/{carePlanId}/careplanupdates");
            var responseContent = await carePlanUpdatesResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
            {
                MessageBox.Show("No care plan updates available for this care plan.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (carePlanUpdatesResponse.IsSuccessStatusCode)
            {
                // Deserialize the response which contains CarePlanUpdates  
                var carePlanUpdatesList = JsonConvert.DeserializeObject<List<CarePlanUpdates>>(responseContent);

                // Populate Care Plan Updates DataGridView  
                dgv_carePlanUpdates.DataSource = carePlanUpdatesList;
                dgv_carePlanUpdates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgv_carePlanUpdates.AutoResizeColumns();

            }
            else
            {
                MessageBox.Show($"Failed to retrieve care plan updates for the selected care plan. Status: {carePlanUpdatesResponse.StatusCode}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void getAppointmentCarePlansAndUpdates(Appointment appointment)
        {
            try
            {
                dgv_carePlans.DataSource = null; // Clear previous data
                dgv_carePlanUpdates.DataSource = null; // Clear previous data

                // Get care plans and updates by appointment ID  
                var carePlanUpdatesResponse = await client.GetAsync($"{apiBaseUrl}/appointment/{appointment.AppointmentId}/careplan/careplanupdates");
                var responseContent = await carePlanUpdatesResponse.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseContent))
                {
                    MessageBox.Show("No care plans or updates available for this appointment.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (carePlanUpdatesResponse.IsSuccessStatusCode)
                {
                    // Deserialize the response which contains CarePlans and CarePlanUpdates  
                    var response = JsonConvert.DeserializeObject<CarePlanResponse>(responseContent);

                    // Populate Care Plans DataGridView
                    dgv_carePlans.DataSource = response.CarePlans;
                    dgv_carePlans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgv_carePlans.AutoResizeColumns();

                    // Populate Care Plan Updates DataGridView  
                    dgv_carePlanUpdates.DataSource = response.CarePlanUpdates;
                    dgv_carePlanUpdates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgv_carePlanUpdates.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show($"Failed to retrieve care plans and updates for the selected appointment. Status: {carePlanUpdatesResponse.StatusCode}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving appointment care plans and updates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_appointments_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_admissions_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_carePlans_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btn_getFullHistory_Click(object sender, EventArgs e)
        {
            dgv_carePlanUpdates.DataSource = null; // Clear previous data
            dgv_carePlans.DataSource = null; // Clear previous data
            dgv_beds.DataSource = null; // Clear previous data
            dgv_vitals.DataSource = null; // Clear previous data
            dgv_appointments.DataSource = null; // Clear previous data
            dgv_admissions.DataSource = null; // Clear previous data
            // Reload the medical history to show all data in main tier
            LoadMedicalHistory();
        }
    }
}
