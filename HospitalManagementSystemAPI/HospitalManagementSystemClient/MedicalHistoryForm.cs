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
            if (_loggedInUser.UserId == this.selectedPatient.PatientOrgId)
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

        private async void btn_getFullHistory_Click(object sender, EventArgs e)
        {
            //get the top grids medical history of the selected patient

            //Admissions
            var admissionResponse = await client.GetAsync($"{apiBaseUrl}/admission/patient/{selectedPatient.PatientId}");
            var admissions = JsonConvert.DeserializeObject<List<Admission>>(await admissionResponse.Content.ReadAsStringAsync());
            if (admissionResponse.IsSuccessStatusCode)
            {
                dgv_admissions.DataSource = admissions;
                dgv_admissions.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else
            {
                MessageBox.Show("Failed to retrieve admissions.");
            }

            //Appointments
            var appointmentResponse = await client.GetAsync($"{apiBaseUrl}/appointment/patient/{selectedPatient.PatientId}");
            if (appointmentResponse.IsSuccessStatusCode)
            {
                var appointmentJson = await appointmentResponse.Content.ReadAsStringAsync();
                var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentJson);
                dgv_appointments.DataSource = appointments;
                dgv_appointments.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else
            {
                MessageBox.Show("Failed to retrieve appointments.");
            }

            //CarePlans
            var carePlanResponse = await client.GetAsync($"{apiBaseUrl}/careplan/patient/{selectedPatient.PatientId}");
            var carePlans = JsonConvert.DeserializeObject<List<CarePlan>>(await carePlanResponse.Content.ReadAsStringAsync());
            if (carePlanResponse.IsSuccessStatusCode)
            {
                dgv_carePlans.DataSource = carePlans;
                dgv_carePlans.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else
            {
                MessageBox.Show("Failed to retrieve careplans.");
            }

            //Vitals
            var vitalsResponse = await client.GetAsync($"{apiBaseUrl}/vitals/patient/{selectedPatient.PatientId}");
            var vitalsJson = await vitalsResponse.Content.ReadAsStringAsync();
            var vitals = JsonConvert.DeserializeObject<List<Vitals>>(vitalsJson);
            if (vitalsResponse.IsSuccessStatusCode)
            {
                dgv_vitals.DataSource = vitals;
                dgv_vitals.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else
            {
                MessageBox.Show("Failed to retrieve vitals.");
            }
        }

        private void btn_historyByDate_Click(object sender, EventArgs e)
        {

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
        }

        private async void getAppointmentCarePlansAndUpdates(Appointment appointment)
        {
            try
            {
                //get carePlans by appointment ID
                var carePlanUpdatesResponse = await client.GetAsync($"{apiBaseUrl}/careplan/appointment/{appointment.AppointmentId}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving appointment care plans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
