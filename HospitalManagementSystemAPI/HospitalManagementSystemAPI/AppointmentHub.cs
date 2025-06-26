using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystemAPI
{
    public class AppointmentHub : Hub
    {
        public async Task SendNotification(string user, string message)
        {
            // Broadcasts a notification to all connected clients.
            await Clients.All.SendAsync("ReceiveAppointmentNotification", user, message);

            await Clients.All.SendAsync("ReceiveAppointmentUpdatedNotification", user, message);

            await Clients.All.SendAsync("ReceiveAppointmentDeletedNotification", user, message);
        }
    }
}
