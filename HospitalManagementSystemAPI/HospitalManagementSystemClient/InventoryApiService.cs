using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using HospitalManagementSystemClient.Models;
using System;

namespace HospitalManagementSystemClient
{
    public class InventoryApiService
    {
        private readonly HttpClient _httpClient;

        public InventoryApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5277"); 
        }

        public async Task<List<InventoryItem>> GetAllItemsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InventoryItem>>("api/inventory");
        }

        public async Task AddItemAsync(InventoryItem item)
        {
            await _httpClient.PostAsJsonAsync("", item);
        }

        public async Task UpdateItemAsync(int id, InventoryItem item)
        {
            await _httpClient.PutAsJsonAsync($"{id}", item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _httpClient.DeleteAsync($"{id}");
        }

    }

    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public string UnitOfMeasure { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
