using Microsoft.AspNetCore.SignalR;

    //SignalR Hub for managing real-time communication in the Hospital Management System.
    // Handles:
    // - User chat
    // - Emergency/system notifications
    // - Live patient vitals updates
    // - Bed availability status broadcasting

namespace HospitalManagementSystemAPI
{
    public class ChatHub : Hub
    {
        
        // Broadcasts a chat message to all connected users.
        // Used for communication between hospital staff or patients.
        
        // <param name="user">The username of the sender.</param>
        // <param name="message">The chat message content.</param>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        
        // Broadcasts a notification to all clients.
        // Can be used for alerts like "Code Blue", appointment changes, or administrative messages.
        
        // <param name="type">The type of notification (e.g., "Emergency", "Info").</param>
        // <param name="content">The actual message or alert.</param>
        public async Task SendNotification(string type, string content)
        {
            await Clients.All.SendAsync("ReceiveNotification", type, content);
        }

        
        
        // Sends real-time patient vitals updates to all connected dashboards or monitoring clients.
        // Suitable for use in ICU monitoring or live medical dashboards.
        // </summary>
        // <param name="patientId">The unique patient identifier.</param>
        // <param name="vitalsData">Formatted vitals data (e.g., heart rate, BP).</param>
        public async Task SendVitalsUpdate(string patientId, string vitalsData)
        {
            await Clients.All.SendAsync("ReceiveVitalsUpdate", patientId, vitalsData);
        }

        // Broadcast bed status to dashboards
        // Sends updates on bed status across all dashboards.
        // Used to reflect real-time changes such as admissions, discharges, or maintenance.
        // <param name="bedId">The unique identifier of the bed (e.g., "BedA12").</param>
        // <param name="status">The new bed status (e.g., "Occupied", "Available").</param>
        public async Task SendBedUpdate(string bedId, string status)
        {
            await Clients.All.SendAsync("ReceiveBedUpdate", bedId, status);
        }
    }
}
