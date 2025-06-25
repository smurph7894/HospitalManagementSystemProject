namespace HospitalManagementSystemClient
{
    partial class MedicalHistoryForm
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
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_Dashboard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_patientId = new System.Windows.Forms.Label();
            this.lbl_patientName = new System.Windows.Forms.Label();
            this.dgv_carePlans = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgv_admissions = new System.Windows.Forms.DataGridView();
            this.labal1 = new System.Windows.Forms.Label();
            this.dgv_vitals = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_beds = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_carePlanUpdates = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgv_appointments = new System.Windows.Forms.DataGridView();
            this.btn_getFullHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_carePlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_admissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vitals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_carePlanUpdates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_appointments)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(12, 12);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(75, 23);
            this.btn_back.TabIndex = 0;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_Dashboard
            // 
            this.btn_Dashboard.Location = new System.Drawing.Point(93, 12);
            this.btn_Dashboard.Name = "btn_Dashboard";
            this.btn_Dashboard.Size = new System.Drawing.Size(100, 23);
            this.btn_Dashboard.TabIndex = 1;
            this.btn_Dashboard.Text = "Dashboard";
            this.btn_Dashboard.UseVisualStyleBackColor = true;
            this.btn_Dashboard.Click += new System.EventHandler(this.btn_Dashboard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patient Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Patient ID:";
            // 
            // lbl_patientId
            // 
            this.lbl_patientId.AutoSize = true;
            this.lbl_patientId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_patientId.Location = new System.Drawing.Point(171, 138);
            this.lbl_patientId.Name = "lbl_patientId";
            this.lbl_patientId.Size = new System.Drawing.Size(0, 25);
            this.lbl_patientId.TabIndex = 7;
            // 
            // lbl_patientName
            // 
            this.lbl_patientName.AutoSize = true;
            this.lbl_patientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_patientName.Location = new System.Drawing.Point(171, 93);
            this.lbl_patientName.Name = "lbl_patientName";
            this.lbl_patientName.Size = new System.Drawing.Size(0, 25);
            this.lbl_patientName.TabIndex = 6;
            // 
            // dgv_carePlans
            // 
            this.dgv_carePlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_carePlans.Location = new System.Drawing.Point(785, 340);
            this.dgv_carePlans.Name = "dgv_carePlans";
            this.dgv_carePlans.RowHeadersWidth = 51;
            this.dgv_carePlans.RowTemplate.Height = 24;
            this.dgv_carePlans.Size = new System.Drawing.Size(700, 254);
            this.dgv_carePlans.TabIndex = 9;
            this.dgv_carePlans.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_carePlans_CellContentClick);
            this.dgv_carePlans.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_carePlans_CellContentClick);
            this.dgv_carePlans.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_carePlans_CellContentClick);
            this.dgv_carePlans.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_carePlans_CellContentClick);
            this.dgv_carePlans.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_carePlans_CellContentClick);
            this.dgv_carePlans.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_carePlans_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(782, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Care Plans";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 309);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Appointments";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(282, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Admissions";
            // 
            // dgv_admissions
            // 
            this.dgv_admissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_admissions.Location = new System.Drawing.Point(285, 37);
            this.dgv_admissions.Name = "dgv_admissions";
            this.dgv_admissions.RowHeadersWidth = 51;
            this.dgv_admissions.RowTemplate.Height = 24;
            this.dgv_admissions.Size = new System.Drawing.Size(709, 254);
            this.dgv_admissions.TabIndex = 16;
            this.dgv_admissions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_admissions_CellContentClick);
            this.dgv_admissions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_admissions_CellContentClick);
            this.dgv_admissions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_admissions_CellContentClick);
            this.dgv_admissions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_admissions_CellContentClick);
            this.dgv_admissions.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_admissions_CellContentClick);
            this.dgv_admissions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_admissions_CellContentClick);
            // 
            // labal1
            // 
            this.labal1.AutoSize = true;
            this.labal1.Location = new System.Drawing.Point(19, 617);
            this.labal1.Name = "labal1";
            this.labal1.Size = new System.Drawing.Size(40, 16);
            this.labal1.TabIndex = 19;
            this.labal1.Text = "Vitals";
            // 
            // dgv_vitals
            // 
            this.dgv_vitals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_vitals.Location = new System.Drawing.Point(22, 636);
            this.dgv_vitals.Name = "dgv_vitals";
            this.dgv_vitals.RowHeadersWidth = 51;
            this.dgv_vitals.RowTemplate.Height = 24;
            this.dgv_vitals.Size = new System.Drawing.Size(685, 254);
            this.dgv_vitals.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1040, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "Beds";
            // 
            // dgv_beds
            // 
            this.dgv_beds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_beds.Location = new System.Drawing.Point(1043, 37);
            this.dgv_beds.Name = "dgv_beds";
            this.dgv_beds.RowHeadersWidth = 51;
            this.dgv_beds.RowTemplate.Height = 24;
            this.dgv_beds.Size = new System.Drawing.Size(376, 254);
            this.dgv_beds.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(782, 628);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "CarePlanUpdates";
            // 
            // dgv_carePlanUpdates
            // 
            this.dgv_carePlanUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_carePlanUpdates.Location = new System.Drawing.Point(785, 647);
            this.dgv_carePlanUpdates.Name = "dgv_carePlanUpdates";
            this.dgv_carePlanUpdates.RowHeadersWidth = 51;
            this.dgv_carePlanUpdates.RowTemplate.Height = 24;
            this.dgv_carePlanUpdates.Size = new System.Drawing.Size(700, 254);
            this.dgv_carePlanUpdates.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1098, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "*Click on an Admission to get Beds recorded.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(903, 628);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(439, 16);
            this.label9.TabIndex = 26;
            this.label9.Text = "*Click on a Care Plan or Appointment to get recorded Care Plan Updates.";
            // 
            // dgv_appointments
            // 
            this.dgv_appointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_appointments.Location = new System.Drawing.Point(22, 340);
            this.dgv_appointments.Name = "dgv_appointments";
            this.dgv_appointments.RowHeadersWidth = 51;
            this.dgv_appointments.RowTemplate.Height = 24;
            this.dgv_appointments.Size = new System.Drawing.Size(709, 254);
            this.dgv_appointments.TabIndex = 28;
            this.dgv_appointments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointments_CellContentClick);
            this.dgv_appointments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointments_CellContentClick);
            this.dgv_appointments.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointments_CellContentClick);
            this.dgv_appointments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointments_CellContentClick);
            this.dgv_appointments.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_appointments_CellContentClick);
            this.dgv_appointments.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_appointments_CellContentClick);
            // 
            // btn_getFullHistory
            // 
            this.btn_getFullHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_getFullHistory.Location = new System.Drawing.Point(22, 202);
            this.btn_getFullHistory.Name = "btn_getFullHistory";
            this.btn_getFullHistory.Size = new System.Drawing.Size(171, 43);
            this.btn_getFullHistory.TabIndex = 29;
            this.btn_getFullHistory.Text = "Get Full History";
            this.btn_getFullHistory.UseVisualStyleBackColor = true;
            this.btn_getFullHistory.Click += new System.EventHandler(this.btn_getFullHistory_Click);
            // 
            // MedicalHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1558, 980);
            this.Controls.Add(this.btn_getFullHistory);
            this.Controls.Add(this.dgv_appointments);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgv_carePlanUpdates);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgv_beds);
            this.Controls.Add(this.labal1);
            this.Controls.Add(this.dgv_vitals);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgv_admissions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgv_carePlans);
            this.Controls.Add(this.lbl_patientId);
            this.Controls.Add(this.lbl_patientName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Dashboard);
            this.Controls.Add(this.btn_back);
            this.Name = "MedicalHistoryForm";
            this.Text = "Patient Medical History";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_carePlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_admissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vitals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_carePlanUpdates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_appointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_Dashboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_patientId;
        private System.Windows.Forms.Label lbl_patientName;
        private System.Windows.Forms.DataGridView dgv_carePlans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_admissions;
        private System.Windows.Forms.Label labal1;
        private System.Windows.Forms.DataGridView dgv_vitals;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_beds;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_carePlanUpdates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgv_appointments;
        private System.Windows.Forms.Button btn_getFullHistory;
    }
}