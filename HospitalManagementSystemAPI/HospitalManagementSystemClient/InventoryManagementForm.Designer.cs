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
            this.btn_addItem = new System.Windows.Forms.Button();
            this.btn_updateItem = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            this.btn_clearFields = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgv_Inventory = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_signout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_unitofmeasure = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nup_reorderLevel = new System.Windows.Forms.NumericUpDown();
            this.txb_location = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmb_IsMedication = new System.Windows.Forms.ComboBox();
            this.cmb_MedicationName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nup_quantityinstock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Inventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_reorderLevel)).BeginInit();
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
            this.txb_description.Location = new System.Drawing.Point(215, 199);
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
            this.label2.Location = new System.Drawing.Point(76, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Quantity in Stock";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Unit of Measure";
            // 
            // nup_quantityinstock
            // 
            this.nup_quantityinstock.Location = new System.Drawing.Point(215, 245);
            this.nup_quantityinstock.Name = "nup_quantityinstock";
            this.nup_quantityinstock.Size = new System.Drawing.Size(283, 22);
            this.nup_quantityinstock.TabIndex = 2;
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
            this.btn_updateItem.Click += new System.EventHandler(this.btn_updateItem_Click);
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.Location = new System.Drawing.Point(562, 150);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(112, 36);
            this.btn_deleteItem.TabIndex = 4;
            this.btn_deleteItem.Text = "Delete Item";
            this.btn_deleteItem.UseVisualStyleBackColor = true;
            this.btn_deleteItem.Click += new System.EventHandler(this.btn_deleteItem_Click);
            // 
            // btn_clearFields
            // 
            this.btn_clearFields.Location = new System.Drawing.Point(562, 192);
            this.btn_clearFields.Name = "btn_clearFields";
            this.btn_clearFields.Size = new System.Drawing.Size(112, 36);
            this.btn_clearFields.TabIndex = 4;
            this.btn_clearFields.Text = "Clear Fields";
            this.btn_clearFields.UseVisualStyleBackColor = true;
            this.btn_clearFields.Click += new System.EventHandler(this.btn_clearFields_Click);
            // 
            // dgv_Inventory
            // 
            this.dgv_Inventory.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dgv_Inventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Inventory.Location = new System.Drawing.Point(52, 466);
            this.dgv_Inventory.Name = "dgv_Inventory";
            this.dgv_Inventory.RowHeadersWidth = 51;
            this.dgv_Inventory.RowTemplate.Height = 24;
            this.dgv_Inventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Inventory.Size = new System.Drawing.Size(964, 263);
            this.dgv_Inventory.TabIndex = 5;
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
            this.btn_back.Location = new System.Drawing.Point(921, 27);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(95, 29);
            this.btn_back.TabIndex = 7;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_signout
            // 
            this.btn_signout.Location = new System.Drawing.Point(921, 73);
            this.btn_signout.Name = "btn_signout";
            this.btn_signout.Size = new System.Drawing.Size(95, 29);
            this.btn_signout.TabIndex = 8;
            this.btn_signout.Text = "Sign Out";
            this.btn_signout.UseVisualStyleBackColor = true;
            this.btn_signout.Click += new System.EventHandler(this.btn_signout_Click);
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
            this.cmb_unitofmeasure.Location = new System.Drawing.Point(215, 285);
            this.cmb_unitofmeasure.Name = "cmb_unitofmeasure";
            this.cmb_unitofmeasure.Size = new System.Drawing.Size(283, 24);
            this.cmb_unitofmeasure.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 336);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Reorder Level";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Location";
            // 
            // nup_reorderLevel
            // 
            this.nup_reorderLevel.Location = new System.Drawing.Point(215, 330);
            this.nup_reorderLevel.Name = "nup_reorderLevel";
            this.nup_reorderLevel.Size = new System.Drawing.Size(283, 22);
            this.nup_reorderLevel.TabIndex = 12;
            // 
            // txb_location
            // 
            this.txb_location.Location = new System.Drawing.Point(215, 365);
            this.txb_location.Name = "txb_location";
            this.txb_location.Size = new System.Drawing.Size(283, 22);
            this.txb_location.TabIndex = 13;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(49, 426);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "Status";
            // 
            // cmb_IsMedication
            // 
            this.cmb_IsMedication.FormattingEnabled = true;
            this.cmb_IsMedication.Location = new System.Drawing.Point(215, 120);
            this.cmb_IsMedication.Name = "cmb_IsMedication";
            this.cmb_IsMedication.Size = new System.Drawing.Size(283, 24);
            this.cmb_IsMedication.TabIndex = 15;
            // 
            // cmb_MedicationName
            // 
            this.cmb_MedicationName.FormattingEnabled = true;
            this.cmb_MedicationName.Location = new System.Drawing.Point(215, 162);
            this.cmb_MedicationName.Name = "cmb_MedicationName";
            this.cmb_MedicationName.Size = new System.Drawing.Size(283, 24);
            this.cmb_MedicationName.TabIndex = 16;
            this.cmb_MedicationName.SelectedIndexChanged += new System.EventHandler(this.comboBox_IsMedication_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(76, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Medication?";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(78, 165);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "Medication Name";
            // 
            // InventoryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 807);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_MedicationName);
            this.Controls.Add(this.cmb_IsMedication);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txb_location);
            this.Controls.Add(this.nup_reorderLevel);
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
            this.Controls.Add(this.nup_quantityinstock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_description);
            this.Controls.Add(this.txb_itemname);
            this.Name = "InventoryManagementForm";
            this.Text = "Inventory Management Form";
            this.Load += new System.EventHandler(this.InventoryManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nup_quantityinstock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Inventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_reorderLevel)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nup_reorderLevel;
        private System.Windows.Forms.TextBox txb_location;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmb_IsMedication;
        private System.Windows.Forms.ComboBox cmb_MedicationName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}