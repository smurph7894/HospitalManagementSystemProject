using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystemAPI
{
    public class AppointmentHub : Hub
    {
        public async Task SendNewAppointmentNotification(string user, string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveAppointmentNotification", user, message);
            } catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public async Task SendAppointmentUpdatedNotification(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveAppointmentUpdatedNotification", user, message);
        }

        public async Task SendAppointmentDeletedNotification(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveAppointmentDeletedNotification", user, message);
        }
    }
}
