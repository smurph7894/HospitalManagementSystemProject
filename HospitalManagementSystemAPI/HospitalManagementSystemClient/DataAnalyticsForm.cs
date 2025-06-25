using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystemClient
{
    public partial class DataAnalyticsForm : Form
    {
        // Reusable HTTP client to call API endpoints
        private readonly HttpClient _httpClient = new HttpClient();

        // Logged-in user (passed from login or dashboard)
        private Users _loggedInUser;
        
        // Constructor takes in the logged-in user and initializes form
        public DataAnalyticsForm(Users user)
        {
            InitializeComponent();

            // Initialize HttpClient for API calls
            _httpClient.BaseAddress = new Uri("http://localhost:5277/api/dataanalytics/"); // Replace port if needed
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _loggedInUser = user; //stores user infor for possible future use 
        }


        //Calls the backend API to retrieve medication usage data and displays it in the DataGridView.
         private async Task LoadMedicationUsageAsync()
        {
            try
            {
                // Sends GET request to the medication usage endpoint
                var response = await _httpClient.GetAsync("medication-usage");

                // If the request succeeds, deserialize the JSON data
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // Deserialize JSON to C# object list
                    var usageList = JsonSerializer.Deserialize<List<MedicationUsageReport>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Bind the result to the DataGridView on the form
                    dgv_Report.DataSource = usageList;
                }
                else
                {
                    // If any exception occurs (e.g., server offline), show error
                    MessageBox.Show("Failed to load data from server.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching analytics: {ex.Message}");
            }
         }

        //Placeholder: Will load patient visit analytics (to be implemented later)
        private void btn_PatientVisits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon: Patient Visits Analytics.");

        }

        /// Placeholder: Will load common ailments analytics (to be implemented later)
        private void btn_CommonAilments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon: Common Ailments Analytics.");
        }

        //Event triggered when the "Medication Usage" button is clicked.
        // Calls LoadMedicationUsageAsync to load and display analytics.
        private async void btn_MedicationUsage_Click(object sender, EventArgs e)
        {
            await LoadMedicationUsageAsync();

        }

        //Form load event - will automatically loads medication usage analytics when form opens.
        private async void DataAnalyticsForm_Load(object sender, EventArgs e)
        {
            await LoadMedicationUsageAsync();

        }

        // Returns user to dashboard when back button is clicked.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();


            // Pass the full Users object to the Dashboard form (or Form1)
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }
    }

    //Model class that represents one row of medication usage analytics data.
    // This structure must match what the API returns.
    internal class MedicationUsageReport
    {
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderLevel { get; set; }
        public bool NeedsRestock { get; set; }
        public int TotalHospitalUsage { get; set; }
    }
}