using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing;

//Nyamburas 
// Real-Time Communication Interface for Hospital Management System
// This form provides a chat interface and handles real-time updates such as:
// - User-to-user chat
// - Emergency notifications
// - Live updates on patient vitals
// - Bed availability changes

namespace HospitalManagementSystemClient
{
    public partial class Form1 : Form
    {
        // SignalR hub connection object for real-time communication
        private HubConnection connection;
        // Logged-in user object, used for identifying sender and managing permissions
        private readonly Users _loggedInUser;
        public Form1(Users user)
        {
            InitializeComponent();

            // Stores the authenticated user passed from login or dashboard
            _loggedInUser = user;

            // Initialize SignalR connection and handlers
            InitializeSignalR();

            // Updates the form title and default user text box
            this.Text = $"Chat - Logged in as {_loggedInUser.Username}";
            txtB_user.Text = _loggedInUser.Username;
        }
        //Establishes a SignalR connection to the ChatHub and registers all real-time event handlers:
        //Chat messages
        //Notifications (emergency, rescheduling, etc.)
        //Vitals updates (ICU/critical care monitoring)
        //Bed status changes (admissions, discharges)
        private async void InitializeSignalR()
        {
            // Creates connection to SignalR hub
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/chathub") // Adjust the URL to your API endpoint
                .Build();

            // Handles incoming chat messages
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Invoke((Action)(() =>
                {
                    string messageText = $"{user}: {message}";

                    listBox.Items.Add(messageText);
                }));
            });
            //  Handle Notifications
            connection.On<string, string>("ReceiveNotification", (type, content) =>
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"{type.ToUpper()} ALERT:\n{content}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }));
            });

            // Handle Vitals Updates (placeholder logic)
            connection.On<string, string>("ReceiveVitalsUpdate", (patientId, vitalsData) =>
            {
                Invoke((Action)(() =>
                {
                    listBox.Items.Add($"Vitals Update for {patientId}: {vitalsData}");
                }));
            });

            //Handle Bed Updates (placeholder logic)
            connection.On<string, string>("ReceiveBedUpdate", (bedId, status) =>
            {
                Invoke((Action)(() =>
                {
                    listBox.Items.Add($"Bed {bedId} status changed: {status}");
                }));
            });

            // Attempts to start connection and report result
            try
            {
                await connection.StartAsync();
                MessageBox.Show("Connected to SignalR server successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR: {ex.Message}");
            }
        }

        // Sends a chat message from the logged-in user to the SignalR hub.
        private async void btn_send_Click(object sender, EventArgs e)
        {
            string user = _loggedInUser.Username;
            string message = txtB_message.Text;

            try
            {
                await connection.InvokeAsync("SendMessage", user, message);
                txtB_message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }
        //Sends a predefined emergency alert to all connected clients.
        private async void btn_Emergency_Click(object sender, EventArgs e)
        {
            
            try
            {
                await connection.InvokeAsync("SendNotification", "Emergency", "Code Blue in ICU!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending notification: {ex.Message}");
            }

        }

        //Sends a test/update for a bed status change(e.g., admission/discharge).
        // This could later be tied to actual admissions logic.
        private async void btn_BedUpdate_Click(object sender, EventArgs e)
        {
            //needs to be updated for when a patient is discharged/admitted 
            await connection.InvokeAsync("SendBedUpdate", "BedA12", "Occupied");

        }

        // Hides the chat form and returns to the dashboard.
        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();


            // Pass the full Users object to the Dashboard form (or Form1)
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();
        }

        //Signs out the current user and returns to the login screen.
        private void btn_signout_Click(object sender, EventArgs e)
        {
            // Show the login form
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // Closes the inventory form
            this.Close();
        }
    }
}
