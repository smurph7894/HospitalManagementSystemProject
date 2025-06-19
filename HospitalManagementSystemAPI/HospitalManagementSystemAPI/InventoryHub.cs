﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using HospitalManagementSystemAPI.Models;

namespace HospitalManagementSystemAPI
{
    public class InventoryHub : Hub
    {
        
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

