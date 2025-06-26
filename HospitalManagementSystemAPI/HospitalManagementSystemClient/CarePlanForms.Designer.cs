namespace HospitalManagementSystemClient
{
    partial class CarePlanForms
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
            this.btn_add_careplan = new System.Windows.Forms.Button();
            this.txtB_carePlanId_CarePlans = new System.Windows.Forms.TextBox();
            this.dataGridView_CarePlans = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_diagnosisDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridView_CarePlanUpdates = new System.Windows.Forms.DataGridView();
            this.btn_dashBoard = new System.Windows.Forms.Button();
            this.btn_update_careplan = new System.Windows.Forms.Button();
            this.btn_delete_careplan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtB_patientId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtB_condition = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker_dateResolved = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtB_carePlanID_updates = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtB_appointmentID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_patientSearch = new System.Windows.Forms.Button();
            this.btn_delete_carePlanUpdate = new System.Windows.Forms.Button();
            this.btn_update_carePlanUpdate = new System.Windows.Forms.Button();
            this.btn_add_carePlanUpdate = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_careplanupdateid = new System.Windows.Forms.Label();
            this.txtB_description = new System.Windows.Forms.TextBox();
            this.txtB_notes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarePlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarePlanUpdates)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_add_careplan
            // 
            this.btn_add_careplan.Location = new System.Drawing.Point(975, 151);
            this.btn_add_careplan.Name = "btn_add_careplan";
            this.btn_add_careplan.Size = new System.Drawing.Size(75, 23);
            this.btn_add_careplan.TabIndex = 0;
            this.btn_add_careplan.Text = "Add";
            this.btn_add_careplan.UseVisualStyleBackColor = true;
            this.btn_add_careplan.Click += new System.EventHandler(this.btn_add_careplan_Click);
            // 
            // txtB_carePlanId_CarePlans
            // 
            this.txtB_carePlanId_CarePlans.Location = new System.Drawing.Point(652, 38);
            this.txtB_carePlanId_CarePlans.Name = "txtB_carePlanId_CarePlans";
            this.txtB_carePlanId_CarePlans.Size = new System.Drawing.Size(296, 22);
            this.txtB_carePlanId_CarePlans.TabIndex = 1;
            // 
            // dataGridView_CarePlans
            // 
            this.dataGridView_CarePlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CarePlans.Location = new System.Drawing.Point(70, 107);
            this.dataGridView_CarePlans.Name = "dataGridView_CarePlans";
            this.dataGridView_CarePlans.RowHeadersWidth = 51;
            this.dataGridView_CarePlans.RowTemplate.Height = 24;
            this.dataGridView_CarePlans.Size = new System.Drawing.Size(410, 250);
            this.dataGridView_CarePlans.TabIndex = 2;
            this.dataGridView_CarePlans.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlans_CellContentClick);
            this.dataGridView_CarePlans.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlans_CellContentClick);
            this.dataGridView_CarePlans.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlans_CellContentClick);
            this.dataGridView_CarePlans.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CarePlans_CellContentClick);
            this.dataGridView_CarePlans.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CarePlans_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Patient Id";
            // 
            // dateTimePicker_diagnosisDate
            // 
            this.dateTimePicker_diagnosisDate.Location = new System.Drawing.Point(652, 176);
            this.dateTimePicker_diagnosisDate.Name = "dateTimePicker_diagnosisDate";
            this.dateTimePicker_diagnosisDate.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_diagnosisDate.TabIndex = 4;
            // 
            // dataGridView_CarePlanUpdates
            // 
            this.dataGridView_CarePlanUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CarePlanUpdates.Location = new System.Drawing.Point(70, 507);
            this.dataGridView_CarePlanUpdates.Name = "dataGridView_CarePlanUpdates";
            this.dataGridView_CarePlanUpdates.RowHeadersWidth = 51;
            this.dataGridView_CarePlanUpdates.RowTemplate.Height = 24;
            this.dataGridView_CarePlanUpdates.Size = new System.Drawing.Size(410, 268);
            this.dataGridView_CarePlanUpdates.TabIndex = 5;
            this.dataGridView_CarePlanUpdates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlanUpdates_CellContentClick);
            this.dataGridView_CarePlanUpdates.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlanUpdates_CellContentClick);
            this.dataGridView_CarePlanUpdates.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CarePlanUpdates_CellContentClick);
            this.dataGridView_CarePlanUpdates.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CarePlanUpdates_CellContentClick);
            this.dataGridView_CarePlanUpdates.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CarePlanUpdates_CellContentClick);
            // 
            // btn_dashBoard
            // 
            this.btn_dashBoard.Location = new System.Drawing.Point(12, 12);
            this.btn_dashBoard.Name = "btn_dashBoard";
            this.btn_dashBoard.Size = new System.Drawing.Size(96, 23);
            this.btn_dashBoard.TabIndex = 6;
            this.btn_dashBoard.Text = "Dashboard";
            this.btn_dashBoard.UseVisualStyleBackColor = true;
            this.btn_dashBoard.Click += new System.EventHandler(this.btn_dashBoard_Click);
            // 
            // btn_update_careplan
            // 
            this.btn_update_careplan.Location = new System.Drawing.Point(975, 207);
            this.btn_update_careplan.Name = "btn_update_careplan";
            this.btn_update_careplan.Size = new System.Drawing.Size(75, 23);
            this.btn_update_careplan.TabIndex = 7;
            this.btn_update_careplan.Text = "Update";
            this.btn_update_careplan.UseVisualStyleBackColor = true;
            this.btn_update_careplan.Click += new System.EventHandler(this.btn_update_careplan_Click);
            // 
            // btn_delete_careplan
            // 
            this.btn_delete_careplan.Location = new System.Drawing.Point(975, 263);
            this.btn_delete_careplan.Name = "btn_delete_careplan";
            this.btn_delete_careplan.Size = new System.Drawing.Size(75, 23);
            this.btn_delete_careplan.TabIndex = 8;
            this.btn_delete_careplan.Text = "Delete";
            this.btn_delete_careplan.UseVisualStyleBackColor = true;
            this.btn_delete_careplan.Click += new System.EventHandler(this.btn_delete_careplan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 479);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Care Plan Updates";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Care Plans";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(566, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Care Plan Id";
            // 
            // txtB_patientId
            // 
            this.txtB_patientId.Location = new System.Drawing.Point(652, 88);
            this.txtB_patientId.Name = "txtB_patientId";
            this.txtB_patientId.Size = new System.Drawing.Size(296, 22);
            this.txtB_patientId.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(574, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Condition";
            // 
            // txtB_condition
            // 
            this.txtB_condition.Location = new System.Drawing.Point(652, 133);
            this.txtB_condition.Name = "txtB_condition";
            this.txtB_condition.Size = new System.Drawing.Size(296, 22);
            this.txtB_condition.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(559, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Description";
            // 
            // dateTimePicker_dateResolved
            // 
            this.dateTimePicker_dateResolved.Location = new System.Drawing.Point(652, 220);
            this.dateTimePicker_dateResolved.Name = "dateTimePicker_dateResolved";
            this.dateTimePicker_dateResolved.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_dateResolved.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(539, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Diagnosis Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(541, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "Date Resolved";
            // 
            // txtB_carePlanID_updates
            // 
            this.txtB_carePlanID_updates.Location = new System.Drawing.Point(652, 600);
            this.txtB_carePlanID_updates.Name = "txtB_carePlanID_updates";
            this.txtB_carePlanID_updates.Size = new System.Drawing.Size(296, 22);
            this.txtB_carePlanID_updates.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(541, 550);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 16);
            this.label10.TabIndex = 25;
            this.label10.Text = "Appointment Id";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(511, 507);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "Care Plan Update Id";
            // 
            // txtB_appointmentID
            // 
            this.txtB_appointmentID.Location = new System.Drawing.Point(652, 550);
            this.txtB_appointmentID.Name = "txtB_appointmentID";
            this.txtB_appointmentID.Size = new System.Drawing.Size(296, 22);
            this.txtB_appointmentID.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(588, 652);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 16);
            this.label12.TabIndex = 27;
            this.label12.Text = "Notes";
            // 
            // btn_patientSearch
            // 
            this.btn_patientSearch.Location = new System.Drawing.Point(114, 12);
            this.btn_patientSearch.Name = "btn_patientSearch";
            this.btn_patientSearch.Size = new System.Drawing.Size(154, 23);
            this.btn_patientSearch.TabIndex = 33;
            this.btn_patientSearch.Text = "Patient Search";
            this.btn_patientSearch.UseVisualStyleBackColor = true;
            this.btn_patientSearch.Click += new System.EventHandler(this.btn_patientSearch_Click);
            // 
            // btn_delete_carePlanUpdate
            // 
            this.btn_delete_carePlanUpdate.Location = new System.Drawing.Point(996, 681);
            this.btn_delete_carePlanUpdate.Name = "btn_delete_carePlanUpdate";
            this.btn_delete_carePlanUpdate.Size = new System.Drawing.Size(75, 23);
            this.btn_delete_carePlanUpdate.TabIndex = 36;
            this.btn_delete_carePlanUpdate.Text = "Delete";
            this.btn_delete_carePlanUpdate.UseVisualStyleBackColor = true;
            this.btn_delete_carePlanUpdate.Click += new System.EventHandler(this.btn_delete_carePlanUpdate_Click);
            // 
            // btn_update_carePlanUpdate
            // 
            this.btn_update_carePlanUpdate.Location = new System.Drawing.Point(996, 625);
            this.btn_update_carePlanUpdate.Name = "btn_update_carePlanUpdate";
            this.btn_update_carePlanUpdate.Size = new System.Drawing.Size(75, 23);
            this.btn_update_carePlanUpdate.TabIndex = 35;
            this.btn_update_carePlanUpdate.Text = "Update";
            this.btn_update_carePlanUpdate.UseVisualStyleBackColor = true;
            this.btn_update_carePlanUpdate.Click += new System.EventHandler(this.btn_update_carePlanUpdate_Click);
            // 
            // btn_add_carePlanUpdate
            // 
            this.btn_add_carePlanUpdate.Location = new System.Drawing.Point(996, 569);
            this.btn_add_carePlanUpdate.Name = "btn_add_carePlanUpdate";
            this.btn_add_carePlanUpdate.Size = new System.Drawing.Size(75, 23);
            this.btn_add_carePlanUpdate.TabIndex = 34;
            this.btn_add_carePlanUpdate.Text = "Add";
            this.btn_add_carePlanUpdate.UseVisualStyleBackColor = true;
            this.btn_add_carePlanUpdate.Click += new System.EventHandler(this.btn_add_carePlanUpdate_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(649, 507);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 16);
            this.label13.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(559, 606);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 16);
            this.label14.TabIndex = 38;
            this.label14.Text = "Care Plan Id";
            // 
            // lbl_careplanupdateid
            // 
            this.lbl_careplanupdateid.AutoSize = true;
            this.lbl_careplanupdateid.Location = new System.Drawing.Point(655, 507);
            this.lbl_careplanupdateid.Name = "lbl_careplanupdateid";
            this.lbl_careplanupdateid.Size = new System.Drawing.Size(0, 16);
            this.lbl_careplanupdateid.TabIndex = 39;
            // 
            // txtB_description
            // 
            this.txtB_description.Location = new System.Drawing.Point(652, 283);
            this.txtB_description.Name = "txtB_description";
            this.txtB_description.Size = new System.Drawing.Size(296, 22);
            this.txtB_description.TabIndex = 40;
            // 
            // txtB_notes
            // 
            this.txtB_notes.Location = new System.Drawing.Point(652, 652);
            this.txtB_notes.Name = "txtB_notes";
            this.txtB_notes.Size = new System.Drawing.Size(296, 22);
            this.txtB_notes.TabIndex = 41;
            // 
            // CarePlanForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 814);
            this.Controls.Add(this.txtB_notes);
            this.Controls.Add(this.txtB_description);
            this.Controls.Add(this.lbl_careplanupdateid);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_delete_carePlanUpdate);
            this.Controls.Add(this.btn_update_carePlanUpdate);
            this.Controls.Add(this.btn_add_carePlanUpdate);
            this.Controls.Add(this.btn_patientSearch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtB_carePlanID_updates);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtB_appointmentID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dateTimePicker_dateResolved);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtB_condition);
            this.Controls.Add(this.txtB_patientId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_delete_careplan);
            this.Controls.Add(this.btn_update_careplan);
            this.Controls.Add(this.btn_dashBoard);
            this.Controls.Add(this.dataGridView_CarePlanUpdates);
            this.Controls.Add(this.dateTimePicker_diagnosisDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_CarePlans);
            this.Controls.Add(this.txtB_carePlanId_CarePlans);
            this.Controls.Add(this.btn_add_careplan);
            this.Name = "CarePlanForms";
            this.Text = "CarePlanForms";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarePlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CarePlanUpdates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add_careplan;
        private System.Windows.Forms.TextBox txtB_carePlanId_CarePlans;
        private System.Windows.Forms.DataGridView dataGridView_CarePlans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_diagnosisDate;
        private System.Windows.Forms.DataGridView dataGridView_CarePlanUpdates;
        private System.Windows.Forms.Button btn_dashBoard;
        private System.Windows.Forms.Button btn_update_careplan;
        private System.Windows.Forms.Button btn_delete_careplan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtB_patientId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtB_condition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker_dateResolved;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtB_carePlanID_updates;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtB_appointmentID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_patientSearch;
        private System.Windows.Forms.Button btn_delete_carePlanUpdate;
        private System.Windows.Forms.Button btn_update_carePlanUpdate;
        private System.Windows.Forms.Button btn_add_carePlanUpdate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_careplanupdateid;
        private System.Windows.Forms.TextBox txtB_description;
        private System.Windows.Forms.TextBox txtB_notes;
    }
}