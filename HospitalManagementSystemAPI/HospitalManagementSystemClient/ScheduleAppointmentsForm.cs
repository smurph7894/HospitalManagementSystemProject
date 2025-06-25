using HospitalManagementSystemClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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

namespace HospitalManagementSystemClient
{
    public partial class ScheduleAppointmentsForm : Form
    {
        private HubConnection _hubConnection;
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        private Appointment selectedAppointment;
        private Patient selectedPatient;
        private Staff _staff;
        public ScheduleAppointmentsForm(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;
            InitializeSignalR();
            loadDataIfPatient(); // Load patient name if the user is a patient
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
                lbl_patientId.Visible = true;
                lbl_patientIdTitle.Visible = true;
                lbl_status.Visible = true;
                comboBox_appointmentStatus.Visible = true;
                // Load staff information if the user is not a patient  
                getStaffID();
            }
        }

        private async void getStaffID()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var staffOrgId = _loggedInUser.UserId.ToString();
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

        private async void InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/appointmentHub") // Ensure this URL matches your API's SignalR hub endpoint
                .Build();
            try
            {
                await _hubConnection.StartAsync();
                MessageBox.Show("Connected to SignalR hub successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR hub: {ex.Message}");
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
                                    lbl_patientId.Text = selectedPatient.PatientId.ToString();
                                    lbl_patientName.Text = selectedPatient.FirstName + " " + selectedPatient.LastName;
                                    dateTimePicker1.Value = selectedAppointment.ScheduledAt;
                                    comboBox_appointmentStatus.SelectedItem = selectedAppointment.Status.ToString();
                                    numericUpDown_duration.Value = selectedAppointment.DurationMinutes;
                                    richTextBox1.Text = selectedAppointment.Reason;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            var appointmentUpdated = new Appointment
            {
                AppointmentId = selectedAppointment.AppointmentId,
                PatientId = selectedPatient.PatientId,
                StaffId = _staff?.StaffId ?? 0, // Use null-coalescing operator to handle null _staff  
                ScheduledAt = dateTimePicker1.Value,
                Status = comboBox_appointmentStatus.SelectedItem != null
                    ? (Status)Enum.Parse(typeof(Status), comboBox_appointmentStatus.SelectedItem.ToString())
                    : Status.Scheduled, // Default to 'Scheduled' if no status is selected  
                DurationMinutes = (int)numericUpDown_duration.Value,
                Reason = richTextBox1.Text
            };
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var appJson = JsonConvert.SerializeObject(appointmentUpdated);
                    var appContent = new StringContent(appJson, Encoding.UTF8, "application/json");
                    var response = client.PostAsync($"{apiBaseUrl}/appointment", appContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var appointmentId = response.Content.ReadAsStringAsync().Result;
                        selectedAppointment = JsonConvert.DeserializeObject<Appointment>(appointmentId);
                        MessageBox.Show($"Appointment added successfully. AppointmentId: {selectedAppointment}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the form fields after adding
                        lbl_patientId.Text = string.Empty;
                        lbl_patientName.Text = string.Empty;
                        dateTimePicker1.Value = DateTime.Now;
                        comboBox_appointmentStatus.SelectedIndex = -1;
                        numericUpDown_duration.Value = 30;
                        richTextBox1.Clear();
                        loadDataIfPatient(); // Reload data if the user is a patient
                    }
                    else
                    {
                        MessageBox.Show("Error adding appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
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
                StaffId = _staff?.StaffId ?? 0, // Use null-coalescing operator to handle null _staff  
                ScheduledAt = dateTimePicker1.Value,
                Status = comboBox_appointmentStatus.SelectedItem != null
                    ? (Status)Enum.Parse(typeof(Status), comboBox_appointmentStatus.SelectedItem.ToString())
                    : Status.Scheduled, // Default to 'Scheduled' if no status is selected  
                DurationMinutes = (int)numericUpDown_duration.Value,
                Reason = richTextBox1.Text
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var appJson = JsonConvert.SerializeObject(appointmentUpdated);
                    var appContent = new StringContent(appJson, Encoding.UTF8, "application/json");
                    var response = client.PutAsync($"{apiBaseUrl}/appointment/{appointmentUpdated.AppointmentId}", appContent).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error updating appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            selectedAppointment = null; // Clear the selected appointment after update
            lbl_patientId.Text = string.Empty; // Clear patient ID label
            lbl_patientName.Text = string.Empty; // Clear patient name label
            dateTimePicker1.Value = DateTime.Now; // Reset date picker to current time
            comboBox_appointmentStatus.SelectedIndex = -1; // Reset appointment status combo box
            numericUpDown_duration.Value = 30; // Reset duration to default value
            richTextBox1.Clear(); // Clear the reason text box
            loadDataIfPatient();
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
                        lbl_patientId.Text = string.Empty;
                        lbl_patientName.Text = string.Empty;
                        dateTimePicker1.Value = DateTime.Now;
                        comboBox_appointmentStatus.SelectedIndex = -1;
                        numericUpDown_duration.Value = 30;
                        richTextBox1.Clear();
                        loadDataIfPatient();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
