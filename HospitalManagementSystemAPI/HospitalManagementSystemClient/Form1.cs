using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace HospitalManagementSystemClient
{
    public partial class Form1 : Form
    {
        private HubConnection connection;
        public Form1()
        {
            InitializeComponent();
            IntializeSignalR();
        }

        private async void IntializeSignalR()
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
            string user = txtB_user.Text;
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
