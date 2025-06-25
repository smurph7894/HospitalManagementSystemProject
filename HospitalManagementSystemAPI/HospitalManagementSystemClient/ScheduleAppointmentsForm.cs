using HospitalManagementSystemClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
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
    public partial class ScheduleAppointmentsForm : Form
    {
        private HubConnection _hubConnection;
        private Users _loggedInUser;
        private readonly string apiBaseUrl = "http://localhost:5277/api";
        private Patient selectedPatient;
        public ScheduleAppointmentsForm(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;
            InitializeSignalR();
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
    }
}
