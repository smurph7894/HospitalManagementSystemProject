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
        private readonly string apiBaseUrl = "http://localhost:5277/api/Patient";
        private Patient selectedPatient;

        private readonly HttpClient client = new HttpClient();

        public MedicalHistoryForm(Users loggedInUser, Patient patientInfo)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            selectedPatient = patientInfo;
        }

        private async void btn_getFullHistory_Click(object sender, EventArgs e)
        {
            //get the top grids medical history of the selected patient

            //Admissions
            var admissionResponse = await client.GetAsync($"{apiBaseUrl}/admissions/patient/{selectedPatient.PatientId}");
            if (admissionResponse.IsSuccessStatusCode)
            {
                var admissionsJson = await admissionResponse.Content.ReadAsStringAsync();
                var admissions = JsonConvert.DeserializeObject<List<Admission>>(admissionsJson);
                // Display admissions in a suitable control, e.g., DataGridView
                dgv_admissions.DataSource = admissions;
            }
            else
            {
                MessageBox.Show("Failed to retrieve admissions.");
            }

            //Appointments
            var appointmentResponse = await client.GetAsync($"{apiBaseUrl}/appointments/patient/{selectedPatient.PatientId}");
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(await appointmentResponse.Content.ReadAsStringAsync());
            if (appointmentResponse.IsSuccessStatusCode)
            {
                // Display appointments in a suitable control, e.g., DataGridView
                dgv_appointments.DataSource = appointments;
            }
            else
            {
                MessageBox.Show("Failed to retrieve appointments.");
            }

            //Vitals
            var vitalsResponse = await client.GetAsync($"{apiBaseUrl}/vitals/patient/{selectedPatient.PatientId}");
            var vitals = JsonConvert.DeserializeObject<List<Vitals>>(await vitalsResponse.Content.ReadAsStringAsync());
            if (vitalsResponse.IsSuccessStatusCode)
            {
                // Display vitals in a suitable control, e.g., DataGridView
                dgv_vitals.DataSource = vitals;
            }
            else
            {
                MessageBox.Show("Failed to retrieve vitals.");
            }
        }

        private void btn_historyByDate_Click(object sender, EventArgs e)
        {

        }
    }
}
