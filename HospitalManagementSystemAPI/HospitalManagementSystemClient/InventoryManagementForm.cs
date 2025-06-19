using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;



namespace HospitalManagementSystemClient 
{ 
    public partial class InventoryManagementForm : Form
    {
        public InventoryManagementForm()
        {
            InitializeComponent();
         
        }

        private InventoryApiService _apiService = new InventoryApiService();
        private async void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            var items = await _apiService.GetAllItemsAsync();
            dgv_Inventory.DataSource = items;

        }

        private async void btn_addItem_Click(object sender, EventArgs e)
        {
            var newItem = new InventoryItem
            {
                Name = txb_itemname.Text,
                Description = txb_description.Text,
                QuantityInStock = int.Parse(nup_quantityinstock.Text),
                UnitOfMeasure = cmb_unitofmeasure.Text,
                ReorderLevel = int.Parse(nup_reorderlevel.Text),
                Location = txb_location.Text,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _apiService.AddItemAsync(newItem);
            MessageBox.Show("Item Added!");
            InventoryManagementForm_Load(sender, e); // Reload grid

        }
    }

   
 
}
