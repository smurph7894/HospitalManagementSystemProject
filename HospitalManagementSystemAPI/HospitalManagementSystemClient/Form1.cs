using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;

namespace HospitalManagementSystemClient
{
    public partial class Form1 : Form
    {
        private HubConnection connection;
        private readonly Users _loggedInUser;
        public Form1(Users user)
        {
            InitializeComponent();
            _loggedInUser = user;

            InitializeSignalR();

            this.Text = $"Chat - Logged in as {_loggedInUser.Username}";
            txtB_user.Text = _loggedInUser.Username;
        }

        private async void InitializeSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/chathub") // Adjust the URL to your API endpoint
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Invoke((Action)(() =>
                {
                    string messageText = $"{user}: {message}";

                    listBox.Items.Add(messageText);
                }));
            });

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
    }
}
