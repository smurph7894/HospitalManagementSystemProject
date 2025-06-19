namespace HospitalManagementSystemClient
{
    partial class InventoryManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txb_itemname = new System.Windows.Forms.TextBox();
            this.txb_description = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nup_quantityinstock = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.btn_updateItem = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            this.btn_clearFields = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgv_Inventory = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityInStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitOfMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReorderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_signout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_unitofmeasure = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nup_reorderlevel = new System.Windows.Forms.NumericUpDown();
            this.txb_location = new System.Windows.Forms.TextBox();
            this.lbl_status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nup_quantityinstock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Inventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_reorderlevel)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_itemname
            // 
            this.txb_itemname.Location = new System.Drawing.Point(215, 80);
            this.txb_itemname.Name = "txb_itemname";
            this.txb_itemname.Size = new System.Drawing.Size(283, 22);
            this.txb_itemname.TabIndex = 0;
            // 
            // txb_description
            // 
            this.txb_description.Location = new System.Drawing.Point(215, 118);
            this.txb_description.Multiline = true;
            this.txb_description.Name = "txb_description";
            this.txb_description.Size = new System.Drawing.Size(283, 28);
            this.txb_description.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Quantity in Stock";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Unit of Measure";
            // 
            // nup_quantityinstock
            // 
            this.nup_quantityinstock.Location = new System.Drawing.Point(215, 164);
            this.nup_quantityinstock.Name = "nup_quantityinstock";
            this.nup_quantityinstock.Size = new System.Drawing.Size(283, 22);
            this.nup_quantityinstock.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(724, 249);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(72, 22);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // btn_addItem
            // 
            this.btn_addItem.Location = new System.Drawing.Point(562, 66);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(112, 36);
            this.btn_addItem.TabIndex = 4;
            this.btn_addItem.Text = "Add Item";
            this.btn_addItem.UseVisualStyleBackColor = true;
            this.btn_addItem.Click += new System.EventHandler(this.btn_addItem_Click);
            // 
            // btn_updateItem
            // 
            this.btn_updateItem.Location = new System.Drawing.Point(562, 108);
            this.btn_updateItem.Name = "btn_updateItem";
            this.btn_updateItem.Size = new System.Drawing.Size(112, 36);
            this.btn_updateItem.TabIndex = 4;
            this.btn_updateItem.Text = "Update Item";
            this.btn_updateItem.UseVisualStyleBackColor = true;
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.Location = new System.Drawing.Point(562, 150);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(112, 36);
            this.btn_deleteItem.TabIndex = 4;
            this.btn_deleteItem.Text = "Delete Item";
            this.btn_deleteItem.UseVisualStyleBackColor = true;
            // 
            // btn_clearFields
            // 
            this.btn_clearFields.Location = new System.Drawing.Point(562, 192);
            this.btn_clearFields.Name = "btn_clearFields";
            this.btn_clearFields.Size = new System.Drawing.Size(112, 36);
            this.btn_clearFields.TabIndex = 4;
            this.btn_clearFields.Text = "Clear Fields";
            this.btn_clearFields.UseVisualStyleBackColor = true;
            // 
            // dgv_Inventory
            // 
            this.dgv_Inventory.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dgv_Inventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Inventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.Name,
            this.Description,
            this.QuantityInStock,
            this.UnitOfMeasure,
            this.ReorderLevel,
            this.Location,
            this.CreatedAt,
            this.UpdatedAt});
            this.dgv_Inventory.Location = new System.Drawing.Point(40, 366);
            this.dgv_Inventory.Name = "dgv_Inventory";
            this.dgv_Inventory.RowHeadersWidth = 51;
            this.dgv_Inventory.RowTemplate.Height = 24;
            this.dgv_Inventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Inventory.Size = new System.Drawing.Size(1180, 222);
            this.dgv_Inventory.TabIndex = 5;
            // 
            // ItemID
            // 
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.MinimumWidth = 6;
            this.ItemID.Name = "ItemID";
            this.ItemID.Width = 125;
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.MinimumWidth = 6;
            this.Name.Name = "Name";
            this.Name.Width = 125;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // QuantityInStock
            // 
            this.QuantityInStock.HeaderText = "Quantity In Stock";
            this.QuantityInStock.MinimumWidth = 6;
            this.QuantityInStock.Name = "QuantityInStock";
            this.QuantityInStock.Width = 125;
            // 
            // UnitOfMeasure
            // 
            this.UnitOfMeasure.HeaderText = "Unit of Measure";
            this.UnitOfMeasure.MinimumWidth = 6;
            this.UnitOfMeasure.Name = "UnitOfMeasure";
            this.UnitOfMeasure.Width = 125;
            // 
            // ReorderLevel
            // 
            this.ReorderLevel.HeaderText = "Reorder Level";
            this.ReorderLevel.MinimumWidth = 6;
            this.ReorderLevel.Name = "ReorderLevel";
            this.ReorderLevel.Width = 125;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.MinimumWidth = 6;
            this.Location.Name = "Location";
            this.Location.Width = 125;
            // 
            // CreatedAt
            // 
            this.CreatedAt.HeaderText = "Created At";
            this.CreatedAt.MinimumWidth = 6;
            this.CreatedAt.Name = "CreatedAt";
            this.CreatedAt.Width = 125;
            // 
            // UpdatedAt
            // 
            this.UpdatedAt.HeaderText = "UpdatedAt";
            this.UpdatedAt.MinimumWidth = 6;
            this.UpdatedAt.Name = "UpdatedAt";
            this.UpdatedAt.Width = 125;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, -15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "label5";
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(750, 28);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(92, 24);
            this.btn_back.TabIndex = 7;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            // 
            // btn_signout
            // 
            this.btn_signout.Location = new System.Drawing.Point(746, 70);
            this.btn_signout.Name = "btn_signout";
            this.btn_signout.Size = new System.Drawing.Size(95, 29);
            this.btn_signout.TabIndex = 8;
            this.btn_signout.Text = "Sign Out";
            this.btn_signout.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(239, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(374, 29);
            this.label6.TabIndex = 9;
            this.label6.Text = "Medical Inventory Management";
            // 
            // cmb_unitofmeasure
            // 
            this.cmb_unitofmeasure.FormattingEnabled = true;
            this.cmb_unitofmeasure.Location = new System.Drawing.Point(215, 204);
            this.cmb_unitofmeasure.Name = "cmb_unitofmeasure";
            this.cmb_unitofmeasure.Size = new System.Drawing.Size(283, 24);
            this.cmb_unitofmeasure.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Reorder Level";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Location";
            // 
            // nup_reorderlevel
            // 
            this.nup_reorderlevel.Location = new System.Drawing.Point(215, 249);
            this.nup_reorderlevel.Name = "nup_reorderlevel";
            this.nup_reorderlevel.Size = new System.Drawing.Size(283, 22);
            this.nup_reorderlevel.TabIndex = 12;
            // 
            // txb_location
            // 
            this.txb_location.Location = new System.Drawing.Point(215, 284);
            this.txb_location.Name = "txb_location";
            this.txb_location.Size = new System.Drawing.Size(283, 22);
            this.txb_location.TabIndex = 13;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(855, 147);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(44, 16);
            this.lbl_status.TabIndex = 14;
            this.lbl_status.Text = "Status";
            // 
            // InventoryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 621);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.txb_location);
            this.Controls.Add(this.nup_reorderlevel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmb_unitofmeasure);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_signout);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgv_Inventory);
            this.Controls.Add(this.btn_clearFields);
            this.Controls.Add(this.btn_deleteItem);
            this.Controls.Add(this.btn_updateItem);
            this.Controls.Add(this.btn_addItem);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.nup_quantityinstock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_description);
            this.Controls.Add(this.txb_itemname);
            
            this.Text = "Inventory Management Form";
            this.Load += new System.EventHandler(this.InventoryManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nup_quantityinstock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Inventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_reorderlevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_itemname;
        private System.Windows.Forms.TextBox txb_description;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nup_quantityinstock;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_addItem;
        private System.Windows.Forms.Button btn_updateItem;
        private System.Windows.Forms.Button btn_deleteItem;
        private System.Windows.Forms.Button btn_clearFields;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_Inventory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_signout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_unitofmeasure;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nup_reorderlevel;
        private System.Windows.Forms.TextBox txb_location;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityInStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitOfMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReorderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedAt;
    }
}