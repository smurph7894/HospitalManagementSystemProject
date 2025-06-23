using HospitalManagementSystemClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystemClient
{
    public partial class PatientUserInfo : Form
    {
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api/Patient";
        private Patient selectedPatient;
        private char[] gender = ['X', 'F', 'M'];

        public PatientUserInfo(Users loggedInUser, Patient patientInfo)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            selectedPatient = patientInfo;
        }

        private void PatientUserInfo_Load(object sender, EventArgs e)
        {
            // Load patient information into the form controls
            if (selectedPatient != null)
            {
                // Populate the labels with patient information
                lbl_patientId.Text = selectedPatient.PatientId.ToString();
                lbl_patientOrgId.Text = selectedPatient.PatientOrgId.ToString();
                lbl_createdAt.Text = selectedPatient.CreatedAt.ToLocalTime().ToString("g");
                lbl_updatedAt.Text = selectedPatient.UpdatedAt.ToLocalTime().ToString("g");

                // Populate the text boxes with patient information
                txtb_firstName.Text = selectedPatient.FirstName;
                txtB_lastName.Text = selectedPatient.LastName;
                txtB_email.Text = selectedPatient.Email;
                txtB_phone.Text = selectedPatient.Phone;
                txtB_address.Text = selectedPatient.Address;
                txtB_emergContactName .Text = selectedPatient.EmergencyContactName;
                txtB_emergContactPhone.Text = selectedPatient.EmergencyContactPhone;
                txtB_InsurProvider .Text = selectedPatient.InsuranceProvider;
                txtB_InsurPolicyNumber.Text = selectedPatient.InsurancePolicyNumber;
                dtp_DOB.Value = selectedPatient.DOB;
                if(selectedPatient.Gender == 'F')
                {
                    comboBox_gender.SelectedIndex = 1;
                }
                else if(selectedPatient.Gender == 'X')
                {
                    comboBox_gender.SelectedIndex = 0;
                }
                else if(selectedPatient.Gender == 'M')
                {
                    comboBox_gender.SelectedIndex = 2;
                }
            }
            else
            {
                MessageBox.Show("No patient information available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (_loggedInUser.UserId == selectedPatient.PatientOrgId)
            {
                // If the logged-in user is the patient, go back to their dashboard
                this.Close();
                var dashBoardForm = new DashBoardForm(_loggedInUser);
                dashBoardForm.Show();
            }
            else
            {
                // If the logged-in user is not the patient, go back to the patient search form
                this.Close();
                var patientSearchForm = new PatientSearchForm(_loggedInUser);
                patientSearchForm.Show();
            }
        }

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            // Navigate to the dashboard form 
            this.Close();
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                // Update patient information logic here
                if (txtb_firstName.Text == "" || txtB_lastName.Text == "" || dtp_DOB.Value == null || comboBox_gender == null)
                {
                    MessageBox.Show("Please fill in all required fields First Name, Last Name, Date of Birth(DOB), and Gender.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var updatedPatient = new Patient
                {
                    PatientId = selectedPatient.PatientId,
                    PatientOrgId = selectedPatient.PatientOrgId,
                    FirstName = txtb_firstName.Text.Trim(),
                    LastName = txtB_lastName.Text.Trim(),
                    Email = txtB_email.Text.Trim(),
                    Phone = txtB_phone.Text.Trim(),
                    Address = txtB_address.Text.Trim(),
                    EmergencyContactName = txtB_emergContactName.Text.Trim(),
                    EmergencyContactPhone = txtB_emergContactPhone.Text.Trim(),
                    InsuranceProvider = txtB_InsurProvider.Text.Trim(),
                    InsurancePolicyNumber = txtB_InsurPolicyNumber.Text.Trim(),
                    DOB = dtp_DOB.Value,
                    Gender = gender[comboBox_gender.SelectedIndex],
                };

                using (HttpClient client = new HttpClient())
                {
                    var response = client.PutAsJsonAsync($"{apiBaseUrl}/{selectedPatient.PatientId}", updatedPatient).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // Update the selected patient with the new information
                        selectedPatient = updatedPatient;
                        MessageBox.Show("Patient information updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating patient information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //check they want to delete the folder
            var confirmResult = MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult != DialogResult.Yes)
            {
                return; // User chose not to delete
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.DeleteAsync($"{apiBaseUrl}/{selectedPatient.PatientOrgId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Patient information deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //delete mongo user
                        var responseMongo = client.DeleteAsync($"https://localhost:5001/api/users/{selectedPatient.PatientOrgId}").Result;
                        if (responseMongo.IsSuccessStatusCode)
                        {
                            MessageBox.Show("User account deleted successfully .", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete user account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //navigate back to the appropriate form based on the logged-in user
                        if (_loggedInUser.UserId == selectedPatient.PatientOrgId)
                        {
                            // If the logged-in user is the patient, redirect to the registration form
                            this.Close();
                            var registerForm = new RegisterForm();
                            registerForm.Show(); // Redirect to the registration form after deletion
                        }
                        else
                        {
                            // If the logged-in user is not the patient, refresh the patient search form
                            this.Close();
                            var patientSearchForm = new PatientSearchForm(_loggedInUser);
                            patientSearchForm.Show();
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
                MessageBox.Show("An error occurred while deleting the patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Delete error: {ex.Message}");
            }
        }
    }
}
