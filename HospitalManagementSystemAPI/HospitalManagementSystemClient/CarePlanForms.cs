using HospitalManagementSystemClient.Models;
using Newtonsoft.Json;
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
    public partial class CarePlanForms : Form
    {
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        private Patient selectedPatient;
        private CarePlanUpdates selectedCarePlanUpdate;
        private CarePlan selectedCarePlan;
        public CarePlanForms(Users loggedInUser, Patient patientInfo)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            selectedPatient = patientInfo;
            CarePlans_Load();
            CarePlanUpdates_Load();
        }

        private readonly HttpClient client = new HttpClient();

        private async void CarePlans_Load()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{apiBaseUrl}/careplan/patient/{selectedPatient.PatientId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<CarePlan> carePlans = JsonConvert.DeserializeObject<List<CarePlan>>(json);
                        dataGridView_CarePlans.DataSource = carePlans;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void CarePlanUpdates_Load()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/careplan/patient/{selectedPatient.PatientId}/careplanupdates");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<CarePlanUpdates> updates = JsonConvert.DeserializeObject<List<CarePlanUpdates>>(json);
                        dataGridView_CarePlanUpdates.DataSource = updates;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void clearEntries()
        {
            txtB_carePlanId_CarePlans.Clear();
            txtB_patientId.Clear();
            dateTimePicker_diagnosisDate.Checked = false;
            dateTimePicker_dateResolved.Checked = false;
            txtB_condition.Clear();
            txtB_description.Clear();
            lbl_careplanupdateid.Text = string.Empty;
            txtB_carePlanID_updates.Clear();
            txtB_appointmentID.Clear();
            txtB_notes.Clear();
        }

        private void updateForm()
        {
            try
            {
                // Clear the entries in the form
                clearEntries();
                // Reload the Care Plans and Care Plan Updates
                CarePlans_Load();
                CarePlanUpdates_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_dashBoard_Click(object sender, EventArgs e)
        {
            // Navigate to the dashboard form 
            this.Close();
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        private void btn_patientSearch_Click(object sender, EventArgs e)
        {
            this.Close();
            var patientSearchForm = new PatientSearchForm(_loggedInUser);
            patientSearchForm.Show();
        }

        private async void btn_add_careplan_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate patient ID  
                if (!int.TryParse(txtB_patientId.Text, out int patientId) || patientId <= 0)
                {
                    MessageBox.Show("Please enter a valid Patient ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newCarePlan = new CarePlan
                {
                    PatientId = patientId,
                    Condition = txtB_condition.Text,
                    Description = txtB_description.Text,
                    DiagnosisDate = dateTimePicker_diagnosisDate.Checked ? dateTimePicker_diagnosisDate.Value : (DateTime?)null,
                    DateResolved = dateTimePicker_dateResolved.Checked ? dateTimePicker_dateResolved.Value : (DateTime?)null
                };

                using (HttpClient client = new HttpClient())
                {
                    // Serialize the CarePlan object to JSON  
                    var jsonContent = JsonConvert.SerializeObject(newCarePlan);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{apiBaseUrl}/careplan", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Care plan created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to create care plan. Status Code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Update the form after adding a care plan
                updateForm();
            }
        }

        private async void btn_update_careplan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtB_carePlanId_CarePlans.Text, out int carePlanId) || carePlanId <= 0)
                {
                    MessageBox.Show("Invalid Care Plan ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var updateDto = new CarePlanUpdateDto
                {
                    Condition = txtB_condition.Text,
                    Description = txtB_description.Text,
                    DiagnosisDate = dateTimePicker_diagnosisDate.Checked ? dateTimePicker_diagnosisDate.Value : (DateTime?)null,
                    DateResolved = dateTimePicker_dateResolved.Checked ? dateTimePicker_dateResolved.Value : (DateTime?)null,
                };


                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(updateDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{apiBaseUrl}/careplan/{carePlanId}/doctor", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Care plan updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to update care plan. Status: {response.StatusCode}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Update the form after updating a care plan
                updateForm();
            }
        }


        private async void btn_delete_careplan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtB_carePlanId_CarePlans.Text, out int carePlanId) || carePlanId <= 0)
                {
                    MessageBox.Show("Please enter a valid CarePlan Id.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure you want to delete Care Plan {carePlanId}?",
                                              "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{apiBaseUrl}/careplan/{carePlanId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Care plan deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to delete care plan. Status: {response.StatusCode}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Update the form after deleting a care plan
                updateForm();
            }
        }

        private async void btn_add_carePlanUpdate_Click(object sender, EventArgs e)
        {
            string CarePlanId = txtB_carePlanID_updates.Text;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonBody = JsonConvert.SerializeObject(new
                    {
                        AppointmentId = txtB_appointmentID.Text,
                        Notes = txtB_notes.Text
                    });
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"http://localhost:5277/api/careplan/{CarePlanId}/careplanupdates", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Care Plan Updates loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btn_update_carePlanUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(lbl_careplanupdateid.Text, out int updateId) || updateId <= 0)
                {
                    MessageBox.Show("Please enter a valid Care Plan Update ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var updateDto = new CarePlanUpdatesDto
                {
                    AppointmentId = int.Parse(txtB_appointmentID.Text),
                    Notes = txtB_notes.Text
                };
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(updateDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{apiBaseUrl}/careplan/careplanupdates/{updateId}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Care plan update updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to update care plan update. Status: {response.StatusCode}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Update the form after updating a care plan update
                updateForm();
            }
        }

        private async void btn_delete_carePlanUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(lbl_careplanupdateid.Text, out int updateId) || updateId <= 0)
                {
                    MessageBox.Show("Please enter a valid Care Plan Update ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure you want to delete Care Plan Update {updateId}?",
                                              "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"{apiBaseUrl}/careplan/careplanupdate/{updateId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Care plan update deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to delete care plan update. Status: {response.StatusCode}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Update the form after deleting a care plan update
                updateForm();
            }
        }

        private void dataGridView_CarePlans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarePlan selectedCarePlan = dataGridView_CarePlans.Rows[e.RowIndex].DataBoundItem as CarePlan;
            if (selectedCarePlan != null)
            {
                txtB_carePlanId_CarePlans.Text = selectedCarePlan.CarePlanId.ToString();
                txtB_patientId.Text = selectedCarePlan.PatientId.ToString();
                if (selectedCarePlan.DiagnosisDate.HasValue)
                {
                    dateTimePicker_diagnosisDate.Value = selectedCarePlan.DiagnosisDate.Value;
                    dateTimePicker_diagnosisDate.Checked = true; // Indicates the date is valid
                }
                else
                {
                    dateTimePicker_diagnosisDate.Checked = false; // Indicates no date is selected
                }
                if (selectedCarePlan.DateResolved.HasValue)
                {
                    dateTimePicker_dateResolved.Value = selectedCarePlan.DateResolved.Value;
                    dateTimePicker_dateResolved.Checked = true; // Indicates the date is valid
                }
                else
                {
                    dateTimePicker_dateResolved.Checked = false; // Indicates no date is selected
                }
                txtB_condition.Text = selectedCarePlan.Condition;
                txtB_description.Text = selectedCarePlan.Description;
            }
            else
            {
                MessageBox.Show("Please select a valid care plan.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView_CarePlanUpdates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarePlanUpdates selectedCarePlanUpdate = dataGridView_CarePlanUpdates.Rows[e.RowIndex].DataBoundItem as CarePlanUpdates;
            if(selectedCarePlanUpdate != null)
            {
                lbl_careplanupdateid.Text = selectedCarePlanUpdate.CarePlanUpdateId.ToString();
                txtB_carePlanID_updates.Text = selectedCarePlanUpdate.CarePlanId.ToString();
                txtB_appointmentID.Text = selectedCarePlanUpdate.AppointmentId.ToString();
                txtB_notes.Text = selectedCarePlanUpdate.Notes;
            }
            else
            {
                MessageBox.Show("Please select a valid care plan update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView_CarePlans_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView_CarePlanUpdates_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }

    public class CarePlanUpdateDto
    {
        public string Condition { get; set; }
        public string Description { get; set; }
        public DateTime? DiagnosisDate { get; set; }
        public DateTime? DateResolved { get; set; }
        public CarePlanUpdatesDto CarePlanUpdate { get; set; }
    }

    public class CarePlanUpdatesDto
    {
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
    }
}

