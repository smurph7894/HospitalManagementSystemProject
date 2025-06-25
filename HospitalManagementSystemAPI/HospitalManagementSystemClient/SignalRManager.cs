using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystemClient
{
    public static class SignalRManager
    {
        public static HubConnection ChatConnection { get; private set; }
        public static HubConnection InventoryConnection { get; private set; }
        public static HubConnection AppointmentConnection { get; private set; }

        private static async Task InitializeSignalR()
        {
            AppointmentConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/AppointmentHub") // Ensure this URL matches your API's SignalR hub endpoint
                .Build();

            //Sally's Inventory Hub Connection
            InventoryConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/InventoryHub")
                .WithAutomaticReconnect()
                .Build();

            //Sallty's Chat Hub Connection
            ChatConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/chathub") // Adjust the URL to your API endpoint
                .Build();

            RegisterHandlers();

            try
            {
                AppointmentConnection.On<string, string>("ReceiveAppointmentNotification", (message, user) =>
                {
                    // Handle the notification received from the hub
                    MessageBox.Show($"Notification: {message} from {user}", "Appointment Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

                await AppointmentConnection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR hub: {ex.Message}");
            }
        }

        private static void RegisterHandlers()
        {
            //item that is added or updated(on)
            InventoryConnection.On<InventoryItem>("ReceiveInventoryUpdate", (updatedItem) =>
                {
                    Invoke((Action)(() =>
                    {
                        InventoryManagementForm.Instance.UpdateInventoryRow(updatedItem);
                        MessageBox.Show($"Item updated or added: Id: {updatedItem.ItemId}, Name:{updatedItem.Name} (Qty: {updatedItem.QuantityInStock})");
                    }));
                });

            //Item that is deleted (on)
            InventoryConnection.On<int>("ReceiveInventoryDelete", (deletedItemId) =>
            {
                Invoke((Action)(() =>
                {
                    foreach (DataGridViewRow row in InventoryManagementForm.Instance.dgv_Inventory.Rows)
                    {
                        if (row.Cells["Id"].Value != null && (int)row.Cells["Id"].Value == deletedItemId)
                        {
                            InventoryManagementForm.Instance.dgv_Inventory.Rows.Remove(row);
                            MessageBox.Show($"Deleted item with ID: {deletedItemId} Name: {deletedItemId}");
                            break;
                        }
                    }
                }));
            });


        }
    }
}


////set up signalR listeners for real time updates
//private async void InitializeSignalR()
//{
//    
//    //item that is added or updated (on)
//    connection.On<InventoryItem>("ReceiveInventoryUpdate", (updatedItem) =>
//    {
//        Invoke((Action)(() =>
//        {
//            UpdateInventoryRow(updatedItem);
//            MessageBox.Show($"Item updated or added: Id: {updatedItem.ItemId}, Name:{updatedItem.Name} (Qty: {updatedItem.QuantityInStock})");
//        }));
//    });

//    //Item that is deleted (on)
//    connection.On<int>("ReceiveInventoryDelete", (deletedItemId) =>
//    {
//        Invoke((Action)(() =>
//        {
//            foreach (DataGridViewRow row in dgv_Inventory.Rows)
//            {
//                if (row.Cells["Id"].Value != null && (int)row.Cells["Id"].Value == deletedItemId)
//                {
//                    dgv_Inventory.Rows.Remove(row);
//                    MessageBox.Show($"Deleted item with ID: {deletedItemId} Name: {deletedItemId}");
//                    break;
//                }
//            }
//        }));
//    });

//    try
//    {
//        await connection.StartAsync();
//        MessageBox.Show("Connected to SignalR successfully.");
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show("Error connecting to SignalR: " + ex.Message);
//    }
//}