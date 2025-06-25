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
            this.Close();
            var form = new PatientUserInfo(_loggedInUser, selectedPatient);
            form.Show();
        }

        private async void btn_getFullHistory_Click(object sender, EventArgs e)
        {
            //get the top grids medical history of the selected patient

            //Admissions
            var admissionResponse = await client.GetAsync($"{apiBaseUrl}/admission/patient/{selectedPatient.PatientId}");
            var admissions = JsonConvert.DeserializeObject<List<Admission>>(await admissionResponse.Content.ReadAsStringAsync());
            if (admissionResponse.IsSuccessStatusCode)
            {
                // Display admissions in a suitable control, e.g., DataGridView
                dgv_admissions.DataSource = admissions;
                dgv_admissions.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else if(admissionResponse.StatusCode == HttpStatusCode.NotFound)
            {
                dgv_admissions.DataSource = null; // Clear the DataGridView if no admissions found
                dgv_admissions.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                //TODO add invisible label to show no admissions found
            }
            else
            {
                MessageBox.Show("Failed to retrieve admissions.");
            }

            //Appointments
            var appointmentResponse = await client.GetAsync($"{apiBaseUrl}/appointment/patient/{selectedPatient.PatientId}");
            var appointmentJson = await appointmentResponse.Content.ReadAsStringAsync();
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentJson);
            if (appointmentResponse.IsSuccessStatusCode)
            {
                // Display appointments in a suitable control, e.g., DataGridView
                dgv_appointments.DataSource = appointments;
                dgv_appointments.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else if (appointmentResponse.StatusCode == HttpStatusCode.NotFound)
            {
                dgv_appointments.DataSource = null; // Clear the DataGridView if no appointments found
                dgv_appointments.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                //TODO add invisible label to show no appointments found
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
                // Display vitals in a suitable control, e.g., DataGridView
                dgv_carePlans.DataSource = carePlans;
                dgv_carePlans.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            }
            else if (carePlanResponse.StatusCode == HttpStatusCode.NotFound)
            {
                dgv_carePlans.DataSource = null; // Clear the DataGridView if no careplans found
                dgv_carePlans.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                //TODO add invisible label to show no careplans found
            }
            else
            {
                MessageBox.Show("Failed to retrieve careplans.");
            }
        }

        private void btn_historyByDate_Click(object sender, EventArgs e)
        {

        }


    }
}
