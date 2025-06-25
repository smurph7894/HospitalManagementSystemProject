using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystemAPI
{
    public class AppointmentHub : Hub
    {
        // This hub can be used to manage real-time appointment scheduling and updates.
        // Currently, it does not implement any specific methods, but can be extended as needed.

        // Example methods could include:
        // - Notify clients of new appointments
        // - Update existing appointments
        // - Cancel appointments

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
