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
                //dgv_admissions.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
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
                //dgv_appointments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill); ;
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
                //dgv_carePlans.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill); 
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
                //dgv_vitals.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
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
        }

        private async void getAdmissionBeds(int bedId)
        {
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
                dgv_beds.DataSource = bedDetails;
                //dgv_beds.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            }
            else
            {
                MessageBox.Show("Failed to retrieve bed details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private async void getCarePlanUpdatesByCarePlan(int carePlanId)
        //{
        //    var carePlanUpdatesResponse = await client.GetAsync($"{apiBaseUrl}/careplan/updates/{carePlanId}");
        //    var responseContent = await carePlanUpdatesResponse.Content.ReadAsStringAsync();

        private async void getAppointmentCarePlansAndUpdates(Appointment appointment)
        {
            try
            {
                // Get carePlans by appointment ID  
                var carePlanUpdatesResponse = await client.GetAsync($"{apiBaseUrl}/careplan/appointment/{appointment.AppointmentId}");
                var responseContent = await carePlanUpdatesResponse.Content.ReadAsStringAsync();

                if (carePlanUpdatesResponse.IsSuccessStatusCode)
                {
                    // Deserialize the response which contains both CarePlans and CarePlanUpdates  
                    var response = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    // Extract CarePlans from the response  
                    var carePlans = JsonConvert.DeserializeObject<List<CarePlan>>(response.carePlans.ToString());

                    // Extract CarePlanUpdates from the response  
                    var carePlanUpdates = JsonConvert.DeserializeObject<List<CarePlanUpdates>>(response.carePlanUpdates.ToString());

                    // Populate Care Plans DataGridView
                    var carePlanList = new List<object>();
                    foreach (var cp in carePlans)
                    {
                        carePlanList.Add(new
                        {
                            CarePlanId = cp.CarePlanId,
                            PatientId = cp.PatientId,
                            Condition = cp.Condition,
                            Description = cp.Description,
                            DiagnosisDate = cp.DiagnosisDate,
                            DateResolved = cp.DateResolved,
                            CreatedAt = cp.CreatedAt,
                            UpdatesCount = cp.CarePlanUpdates != null ? cp.CarePlanUpdates.Count : 0
                        });
                    }
                    dgv_carePlans.DataSource = carePlanList;
                    //dgv_carePlans.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Populate Care Plan Updates DataGridView  
                    var carePlanUpdatesList = new List<object>();
                    foreach (var cpu in carePlanUpdates)
                    {
                        carePlanUpdatesList.Add(new
                        {
                            CarePlanUpdateId = cpu.CarePlanUpdateId,
                            AppointmentId = cpu.AppointmentId,
                            Notes = cpu.Notes
                        });
                    }
                    dgv_carePlanUpdates.DataSource = carePlanUpdatesList;
                    //dgv_carePlanUpdates.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                else
                {
                    MessageBox.Show($"Failed to retrieve care plans for the selected appointment. Status: {carePlanUpdatesResponse.StatusCode}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving appointment care plans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
