using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//Nyamburas
//Windows form UI for interacting with inventory via REST APi and SignalR

namespace HospitalManagementSystemClient 
{ 
    public partial class InventoryManagementForm : Form
    {
        private HubConnection connection;
        private readonly string apiBaseUrl = "http://localhost:5277/api/Inventory";
        private Users _loggedInUser;
        public InventoryManagementForm(Users user)
        {
            InitializeComponent();
            InitializeGrid();
            InitializeSignalR();
            LoadInventory();
            _loggedInUser = user;

            dgv_Inventory.SelectionChanged += dgv_Inventory_SelectionChanged;


            lblStatus.Visible = false; // start hidden

        }

        //Setup for columns for inventory datagridview
        private void InitializeGrid()
        {
            dgv_Inventory.Columns.Add("ItemId", "ID");
            dgv_Inventory.Columns.Add("Name", "Name");
            dgv_Inventory.Columns.Add("Description", "Description");
            dgv_Inventory.Columns.Add("QuantityInStock", "Quantity");
            dgv_Inventory.Columns.Add("UnitOfMeasure", "Unit");
            dgv_Inventory.Columns.Add("ReorderLevel", "Reorder Level");
            dgv_Inventory.Columns.Add("Location", "Location");
            dgv_Inventory.Columns.Add("CreatedAt", "Created At");
            dgv_Inventory.Columns.Add("UpdatedAt", "Last Updated");
        }

        //Retrieves all inventory items from the API
        private async void LoadInventory()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/all");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var inventory = JsonConvert.DeserializeObject<List<InventoryItem>>(json);
                    DisplayInventory(inventory);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory:\n" + ex.Message + "\n\n" + ex.StackTrace);
                }
            }
        }

        //Populates the grid with inventory Items
        private void DisplayInventory(List<InventoryItem> items)
        {
            dgv_Inventory.Rows.Clear();

            foreach (var item in items)
            {
                int rowIndex = dgv_Inventory.Rows.Add(
                    item.ItemId,
                    item.Name,
                    item.Description,
                    item.QuantityInStock,
                    item.UnitOfMeasure,
                    item.ReorderLevel,
                    item.Location,
                    item.CreatedAt,
                    item.UpdatedAt
                );

                //Highlights low stock
                if (item.QuantityInStock < 10)
                {
                    dgv_Inventory.Rows[rowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                    lblStatus.Visible = true;
                    lblStatus.Text = $"Low in Quantity Stock: Id: {item.ItemId} Name: {item.Name} ";
                }
            }
        }

        //When the user selects a row, it populates the text fields for editing
        private void dgv_Inventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Inventory.CurrentRow == null) return;
           

            try
            {
                // Populate textboxes and controls from DataGridView row
                txb_itemname.Text = dgv_Inventory.CurrentRow.Cells["Name"].Value?.ToString();
                txb_description.Text = dgv_Inventory.CurrentRow.Cells["Description"].Value?.ToString();
                txb_location.Text = dgv_Inventory.CurrentRow.Cells["Location"].Value?.ToString();
                cmb_unitofmeasure.Text = dgv_Inventory.CurrentRow.Cells["UnitOfMeasure"].Value?.ToString();

                // parsing numeric controls
                if (int.TryParse(dgv_Inventory.CurrentRow.Cells["QuantityInStock"].Value?.ToString(), out int qty))
                    nup_quantityinstock.Value = qty;

                if (int.TryParse(dgv_Inventory.CurrentRow.Cells["ReorderLevel"].Value?.ToString(), out int reorder))
                    nup_reorderLevel.Value = reorder;

                // show CreatedAt and UpdatedAt in tooltips :
                var createdAt = dgv_Inventory.CurrentRow.Cells["CreatedAt"].Value?.ToString();
                var updatedAt = dgv_Inventory.CurrentRow.Cells["UpdatedAt"].Value?.ToString();

                
                toolTip1.SetToolTip(txb_itemname, $"Created: {createdAt}, Updated: {updatedAt}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load item details from the selected row:\n" + ex.Message);
            }
        }

        //set up signalR listeners for real time updates
        private async void InitializeSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5277/inventoryHub") 
                .WithAutomaticReconnect()
                .Build();
            //item that is added or updated (on)
            connection.On<InventoryItem>("ReceiveInventoryUpdate", (updatedItem) =>
            {
                Invoke((Action)(() =>
                {
                    UpdateInventoryRow(updatedItem);
                    MessageBox.Show($"Item updated or added: Id: {updatedItem.ItemId}, Name:{updatedItem.Name} (Qty: {updatedItem.QuantityInStock})");
                }));
            });

            //Item that is deleted (on)
            connection.On<int>("ReceiveInventoryDelete", (deletedItemId) =>
            {
                Invoke((Action)(() =>
                {
                    foreach (DataGridViewRow row in dgv_Inventory.Rows)
                    {
                        if (row.Cells["Id"].Value != null && (int)row.Cells["Id"].Value == deletedItemId)
                        {
                            dgv_Inventory.Rows.Remove(row);
                            MessageBox.Show($"Deleted item with ID: {deletedItemId} Name: {deletedItemId}");
                            break;
                        }
                    }
                }));
            });

            try
            {
                await connection.StartAsync();
                MessageBox.Show("Connected to SignalR successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to SignalR: " + ex.Message);
            }
        }




      
        
        //Adding Item via API
        private async void btn_addItem_Click(object sender, EventArgs e)
        {
            var newItem = new InventoryItem
            {
                Name = txb_itemname.Text,
                Description = txb_description.Text,  
                QuantityInStock = (int)nup_quantityinstock.Value,
                UnitOfMeasure = cmb_unitofmeasure.Text,
                ReorderLevel = (int)nup_reorderLevel.Value,
                Location = txb_location.Text,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{apiBaseUrl}/add", content);
                if (response.IsSuccessStatusCode)
                {
                    LoadInventory(); // Refresh
                }
                else
                {
                    MessageBox.Show("Add failed: " + response.ReasonPhrase);
                }
            }

        }

        //Updating Item via API
        private async void btn_updateItem_Click(object sender, EventArgs e)
        {
            if (dgv_Inventory.CurrentRow == null) return;

            int id = Convert.ToInt32(dgv_Inventory.CurrentRow.Cells["ItemId"].Value);

            var updatedItem = new InventoryItem
            {
                ItemId = id,
                Name = txb_itemname.Text,
                Description = txb_description.Text,
                QuantityInStock = (int)nup_quantityinstock.Value,
                UnitOfMeasure = cmb_unitofmeasure.Text,
                ReorderLevel = (int)nup_reorderLevel.Value,
                Location = txb_location.Text,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(updatedItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"{apiBaseUrl}/{updatedItem.ItemId}", content);
                if (response.IsSuccessStatusCode)
                {
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Update failed: " + response.ReasonPhrase);
                }
            }

        }

        //Updating or adding row to datagridview 
        private void UpdateInventoryRow(InventoryItem item)
        {
            // Trying to find row with ItemId
            foreach (DataGridViewRow row in dgv_Inventory.Rows)
            {
                if (row.Cells["ItemId"].Value != null && (int)row.Cells["ItemId"].Value == item.ItemId)
                {
                    row.Cells["Name"].Value = item.Name;
                    row.Cells["Description"].Value = item.Description;
                    row.Cells["QuantityInStock"].Value = item.QuantityInStock;
                    row.Cells["UnitOfMeasure"].Value = item.UnitOfMeasure;
                    row.Cells["ReorderLevel"].Value = item.ReorderLevel;
                    row.Cells["Location"].Value = item.Location;
                    row.Cells["CreatedAt"].Value = item.CreatedAt;
                    row.Cells["UpdatedAt"].Value = item.UpdatedAt;
                    return;
                }
            }
            dgv_Inventory.Rows.Add(
                item.ItemId,
                item.Name,
                item.Description,
                item.QuantityInStock,
                item.UnitOfMeasure,
                item.ReorderLevel,
                item.Location,
                item.CreatedAt,
                item.UpdatedAt
            );

            
        }

        //Deletig via API
        private async void btn_deleteItem_Click(object sender, EventArgs e )
        {
            if (dgv_Inventory.CurrentRow == null) return;

            int id = Convert.ToInt32(dgv_Inventory.CurrentRow.Cells["ItemId"].Value);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{apiBaseUrl}/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    
                    
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Delete failed: " + response.ReasonPhrase);
                }
            }
            


        }

        //Populates units of measure on form load
        private void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            cmb_unitofmeasure.Items.AddRange(new[] {
                "Box",
                "Pack",
                "Tablet",
                "Vial",
                "Syringe",
                "Bottle",
                "Tube",
                "Piece",
            });

        }

        //Signout Button goes back to login page
        private void btn_signout_Click(object sender, EventArgs e)
        {
            // Show the login form
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // Closes the inventory form
            this.Close(); 



        }

        //Clear button clears all fields for new addition
        private void btn_clearFields_Click(object sender, EventArgs e)
        {
            // Clear all TextBoxes
            txb_itemname.Clear();
            txb_description.Clear();
            txb_location.Clear();

            // Reset ComboBox
            cmb_unitofmeasure.SelectedIndex = -1;

            // Reset NumericUpDowns
            nup_quantityinstock.Value = nup_quantityinstock.Minimum;
            nup_reorderLevel.Value = nup_reorderLevel.Minimum;

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();

           
            // Pass the full Users object to the Dashboard form (or Form1)
            var dashBoardForm = new DashBoardForm(_loggedInUser);
            dashBoardForm.Show();

        }
    }

    //Inventory Model with Validation attributes
    internal class InventoryItem
    {
        public int ItemId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int QuantityInStock { get; set; } = 0;

        [StringLength(50)]
        public string UnitOfMeasure { get; set; }

        [Required]
        public int ReorderLevel { get; set; } = 0;

        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
