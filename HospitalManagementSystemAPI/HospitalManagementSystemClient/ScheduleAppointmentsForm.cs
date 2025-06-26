using HospitalManagementSystemClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HospitalManagementSystemClient
{
    public partial class ScheduleAppointmentsForm : Form
    {
        public static HubConnection AppointmentConnection;
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        private Appointment selectedAppointment;
        private Patient selectedPatient;
        private Staff _staff;
        public ScheduleAppointmentsForm(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;
            loadDataIfPatient(); // Load patient name if the user is a patient
            InitializeSignalR(); 
        }

        private void loadDataIfPatient()
        {
            // Check if the user is a patient
            if (_loggedInUser.Roles.Contains(Role.Patient))
            {
                // Load the patient's information
                lbl_patientName.Text = _loggedInUser.Username;
            }
            else
            {
                txtB_patientId.Visible = true;
                lbl_patientIdTitle.Visible = true;
                lbl_status.Visible = true;
                comboBox_appointmentStatus.Visible = true;
                // Load staff information if the user is not a patient  
                getStaffID();
            }
        }

        private async Task InitializeSignalR()
        {
            AppointmentConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/appointmenthub") // Ensure this URL matches your API's SignalR hub endpoint
                .Build();

            AppointmentConnection.On<string, string>("ReceiveAppointmentNotification", (patientId, message) =>
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"New appointment scheduled for {patientId}: {message}", "Appointment Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });

            AppointmentConnection.On<string, string>("ReceiveAppointmentUpdatedNotification", (patientId, message) =>
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Appointment updated for {patientId}: {message}", "Appointment Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });

            AppointmentConnection.On<string, string>("ReceiveAppointmentDeletedNotification", (patientId, message) =>
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Appointment deleted for {patientId}: {message}", "Appointment Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });

            try
            {
                await AppointmentConnection.StartAsync();
                MessageBox.Show("Connected to Appointment SignalR successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to Appointment SignalR hub: {ex.Message}");
            }
        }

        private async void getStaffID()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var staffOrgId = _loggedInUser.UserId.ToString();
                    Console.WriteLine($"staff userId {staffOrgId}");
                    var response = await client.GetAsync($"{apiBaseUrl}/staff/{staffOrgId}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var staff = JsonConvert.DeserializeObject<Staff>(json);
                        if (staff != null)
                        {
                            _staff = staff;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving staff details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving staff details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task getPatientId()
        {
            var patientOrgId = _loggedInUser.UserId.ToString();
            Console.WriteLine($"patientOrgId: {patientOrgId}");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/patient/userId/{patientOrgId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        selectedPatient = JsonConvert.DeserializeObject<Patient>(json);
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving patient details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving patient details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            this.Close();
            // Pass the full Users object to the Dashboard form (or Form1)
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            var appointmentId = Int32.Parse(txtB_patientSearch.Text);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync($"{apiBaseUrl}/appointment/{appointmentId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        selectedAppointment = JsonConvert.DeserializeObject<Appointment>(json);
                        if (selectedAppointment != null)
                        {
                            int patientId = selectedAppointment.PatientId;
                            // Fetch patient details using the patientId from the appointment
                            var patientResponse = client.GetAsync($"{apiBaseUrl}/patient/{patientId}").Result;
                            if (patientResponse.IsSuccessStatusCode)
                            {
                                var patientJson = patientResponse.Content.ReadAsStringAsync().Result;
                                selectedPatient = JsonConvert.DeserializeObject<Patient>(patientJson);
                                if (selectedPatient != null)
                                {
                                    // Display appointment and patient details
                                    txtB_patientId.Text = selectedPatient.PatientId.ToString();
                                    lbl_patientName.Text = $"{selectedPatient.FirstName} {selectedPatient.LastName}";
                                    dateTimePicker1.Value = selectedAppointment.ScheduledAt;
                                    numericUpDown_duration.Value = selectedAppointment.DurationMinutes;
                                    richTextBox1.Text = selectedAppointment.Reason;
                                    var statusString = selectedAppointment.Status.ToString();
                                    if (Enum.TryParse(statusString, out Status parsedStatus))
                                    {
                                        comboBox_appointmentStatus.SelectedIndex = (int)parsedStatus;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error retrieving patient details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error retrieving appointment. Please check the ID and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                txtB_patientSearch.Clear(); // Clear the search box after searching
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validate_appointment(Appointment appointment)
        {
            if (appointment == null)
            {
                MessageBox.Show("Appointment cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(appointment.Reason == null || appointment.Reason.Trim().Length == 0)
            {
                MessageBox.Show("Reason for appointment cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (appointment.ScheduledAt == null)
            {
                MessageBox.Show("Scheduled date and time cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(appointment.DurationMinutes < 30 || appointment.DurationMinutes>90)
            {
                MessageBox.Show("Duration must be between 30 and 90 minutes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(appointment.Status == null)
            {
                MessageBox.Show("Status cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using(HttpClient client = new HttpClient())
            {
                var checkUserExistsResponse = client.GetAsync($"{apiBaseUrl}/patient/{appointment.PatientId}").Result;
                if (!checkUserExistsResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Patient does not exist. Please enter a valid Patient ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private async void btn_Add_Click(object sender, EventArgs e)
        {
            int appointmentPatientId;

            if (_loggedInUser.Roles.Contains(Role.Patient))
            {
                await getPatientId(); // Await the async call  
                if (selectedPatient == null)
                {
                    MessageBox.Show("Patient not found. Cannot create appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                appointmentPatientId = selectedPatient.PatientId;
            }
            else
            {
                if (string.IsNullOrEmpty(txtB_patientId.Text) || !int.TryParse(txtB_patientId.Text, out int patientId))
                {
                    MessageBox.Show("Please enter a valid Patient ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                appointmentPatientId = patientId;
            }
            Appointment appointmentUpdated = new Appointment
            {
                PatientId = appointmentPatientId,
                StaffId = _staff?.StaffId, // Use null-coalescing operator to handle null _staff    
                ScheduledAt = dateTimePicker1.Value,
                Status = comboBox_appointmentStatus.SelectedItem != null
                    ? (Status)Enum.Parse(typeof(Status), comboBox_appointmentStatus.SelectedItem.ToString())
                    : Status.Scheduled, // Default to 'Scheduled' if no status is selected    
                DurationMinutes = (int)numericUpDown_duration.Value,
                Reason = richTextBox1.Text,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            validate_appointment(appointmentUpdated);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.PostAsJsonAsync($"{apiBaseUrl}/appointment", appointmentUpdated).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the form fields after adding  
                        txtB_patientId.Text = string.Empty;
                        lbl_patientName.Text = string.Empty;
                        dateTimePicker1.Value = DateTime.Now;
                        comboBox_appointmentStatus.SelectedIndex = -1;
                        numericUpDown_duration.Value = 30;
                        richTextBox1.Clear();
                        if (_loggedInUser.Roles.Contains(Role.Patient))
                        {
                            // Load the patient's information
                            lbl_patientName.Text = _loggedInUser.Username;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error adding appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Notify SignalR hub about the new appointment
                await AppointmentConnection.InvokeAsync("SendNewAppointmentNotification", appointmentPatientId.ToString(), appointmentUpdated.Reason);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            var appointmentUpdated = new Appointment
            {
                AppointmentId = selectedAppointment.AppointmentId,
                PatientId = selectedPatient.PatientId,
                StaffId = _staff?.StaffId, // Use null-coalescing operator to handle null _staff  
                ScheduledAt = dateTimePicker1.Value,
                DurationMinutes = (int)numericUpDown_duration.Value,
                Reason = richTextBox1.Text
            };
            if (Enum.TryParse<Status>(comboBox_appointmentStatus.SelectedItem?.ToString(), out var status))
            {
                appointmentUpdated.Status = (Status)status;
            }

            validate_appointment(appointmentUpdated);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.PutAsJsonAsync($"{apiBaseUrl}/appointment/{appointmentUpdated.AppointmentId}", appointmentUpdated).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error updating appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Notify SignalR hub about the updated appointment
                AppointmentConnection.InvokeAsync("SendAppointmentUpdatedNotification", selectedPatient.PatientId.ToString(), "An Appointment has been updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            selectedAppointment = null; // Clear the selected appointment after update
            txtB_patientId.Text = string.Empty; // Clear patient ID label
            lbl_patientName.Text = string.Empty; // Clear patient name label
            dateTimePicker1.Value = DateTime.Now; // Reset date picker to current time
            comboBox_appointmentStatus.SelectedIndex = -1; // Reset appointment status combo box
            numericUpDown_duration.Value = 30; // Reset duration to default value
            richTextBox1.Clear(); // Clear the reason text box
            // Check if the user is a patient
            if (_loggedInUser.Roles.Contains(Role.Patient))
            {
                // Load the patient's information
                lbl_patientName.Text = _loggedInUser.Username;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (selectedAppointment == null)
                    {
                        MessageBox.Show("Please select an appointment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var response = client.DeleteAsync($"{apiBaseUrl}/appointment/{selectedAppointment.AppointmentId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Appointment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Clear the form fields after deletion
                        txtB_patientId.Text = string.Empty;
                        lbl_patientName.Text = string.Empty;
                        dateTimePicker1.Value = DateTime.Now;
                        comboBox_appointmentStatus.SelectedIndex = -1;
                        numericUpDown_duration.Value = 30;
                        richTextBox1.Clear();
                        // Check if the user is a patient
                        if (_loggedInUser.Roles.Contains(Role.Patient))
                        {
                            // Load the patient's information
                            lbl_patientName.Text = _loggedInUser.Username;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error deleting appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Notify SignalR hub about the deleted appointment
                AppointmentConnection.InvokeAsync("SendAppointmentDeletedNotification", selectedPatient.PatientId.ToString(), "Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
