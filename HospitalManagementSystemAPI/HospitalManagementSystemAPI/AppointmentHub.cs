using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystemAPI
{
    public class AppointmentHub : Hub
    {
        public async Task SendNotification(string message, string user)
        {
            // Broadcasts a notification to all connected clients.
            // This is for appointment reminders, updates, or cancellations.
            await Clients.All.SendAsync("ReceiveAppointmentNotification", message, user);
        }
    }
}
