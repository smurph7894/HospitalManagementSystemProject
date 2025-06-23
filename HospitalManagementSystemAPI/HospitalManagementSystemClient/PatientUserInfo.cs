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

        }
    }
}
