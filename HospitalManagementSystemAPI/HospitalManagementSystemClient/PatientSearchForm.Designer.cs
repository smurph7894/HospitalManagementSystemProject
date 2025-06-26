namespace HospitalManagementSystemClient
{
    partial class PatientSearchForm
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
            this.txtB_searchText = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_deletePatient = new System.Windows.Forms.Button();
            this.btn_editPatientInfo = new System.Windows.Forms.Button();
            this.dataGridView_PatientList = new System.Windows.Forms.DataGridView();
            this.btn_back = new System.Windows.Forms.Button();
            this.comboBox_searchCategories = new System.Windows.Forms.ComboBox();
            this.btn_getAllPatients = new System.Windows.Forms.Button();
            this.btn_viewPatientHistory = new System.Windows.Forms.Button();
            this.btn_patientCarePlans = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PatientList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtB_searchText
            // 
            this.txtB_searchText.Location = new System.Drawing.Point(33, 62);
            this.txtB_searchText.Name = "txtB_searchText";
            this.txtB_searchText.Size = new System.Drawing.Size(272, 22);
            this.txtB_searchText.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(437, 63);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(114, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_deletePatient
            // 
            this.btn_deletePatient.Location = new System.Drawing.Point(1238, 63);
            this.btn_deletePatient.Name = "btn_deletePatient";
            this.btn_deletePatient.Size = new System.Drawing.Size(119, 23);
            this.btn_deletePatient.TabIndex = 2;
            this.btn_deletePatient.Text = "Delete Patient";
            this.btn_deletePatient.UseVisualStyleBackColor = true;
            this.btn_deletePatient.Click += new System.EventHandler(this.btn_deletePatient_Click);
            // 
            // btn_editPatientInfo
            // 
            this.btn_editPatientInfo.Location = new System.Drawing.Point(1113, 62);
            this.btn_editPatientInfo.Name = "btn_editPatientInfo";
            this.btn_editPatientInfo.Size = new System.Drawing.Size(119, 23);
            this.btn_editPatientInfo.TabIndex = 3;
            this.btn_editPatientInfo.Text = "Edit Patient Info";
            this.btn_editPatientInfo.UseVisualStyleBackColor = true;
            this.btn_editPatientInfo.Click += new System.EventHandler(this.btn_editPatientInfo_Click);
            // 
            // dataGridView_PatientList
            // 
            this.dataGridView_PatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PatientList.Location = new System.Drawing.Point(12, 94);
            this.dataGridView_PatientList.Name = "dataGridView_PatientList";
            this.dataGridView_PatientList.RowHeadersWidth = 51;
            this.dataGridView_PatientList.RowTemplate.Height = 24;
            this.dataGridView_PatientList.Size = new System.Drawing.Size(1373, 701);
            this.dataGridView_PatientList.TabIndex = 4;
            this.dataGridView_PatientList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_PatientList_CellContentClick);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(12, 12);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(75, 23);
            this.btn_back.TabIndex = 5;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // comboBox_searchCategories
            // 
            this.comboBox_searchCategories.FormattingEnabled = true;
            this.comboBox_searchCategories.Items.AddRange(new object[] {
            "",
            "PatientId",
            "PatientOrgId",
            "DOB",
            "Gender",
            "FirstName",
            "LastName",
            "Phone",
            "Email",
            "Address",
            "EmergencyContactName",
            "EmergencyContactPhone",
            "InsuranceProvider",
            "InsurancePolicyNumber"});
            this.comboBox_searchCategories.Location = new System.Drawing.Point(311, 62);
            this.comboBox_searchCategories.Name = "comboBox_searchCategories";
            this.comboBox_searchCategories.Size = new System.Drawing.Size(121, 24);
            this.comboBox_searchCategories.TabIndex = 6;
            this.comboBox_searchCategories.SelectedIndexChanged += new System.EventHandler(this.selected_category_Changed);
            // 
            // btn_getAllPatients
            // 
            this.btn_getAllPatients.Location = new System.Drawing.Point(567, 63);
            this.btn_getAllPatients.Name = "btn_getAllPatients";
            this.btn_getAllPatients.Size = new System.Drawing.Size(119, 23);
            this.btn_getAllPatients.TabIndex = 7;
            this.btn_getAllPatients.Text = "Get All Patients";
            this.btn_getAllPatients.UseVisualStyleBackColor = true;
            this.btn_getAllPatients.Click += new System.EventHandler(this.btn_getAllPatients_Click);
            // 
            // btn_viewPatientHistory
            // 
            this.btn_viewPatientHistory.Location = new System.Drawing.Point(716, 61);
            this.btn_viewPatientHistory.Name = "btn_viewPatientHistory";
            this.btn_viewPatientHistory.Size = new System.Drawing.Size(210, 23);
            this.btn_viewPatientHistory.TabIndex = 8;
            this.btn_viewPatientHistory.Text = "View Patient Medical History";
            this.btn_viewPatientHistory.UseVisualStyleBackColor = true;
            this.btn_viewPatientHistory.Click += new System.EventHandler(this.btn_viewPatientHistory_Click);
            // 
            // btn_patientCarePlans
            // 
            this.btn_patientCarePlans.Location = new System.Drawing.Point(932, 61);
            this.btn_patientCarePlans.Name = "btn_patientCarePlans";
            this.btn_patientCarePlans.Size = new System.Drawing.Size(130, 23);
            this.btn_patientCarePlans.TabIndex = 9;
            this.btn_patientCarePlans.Text = "Patient CarePlans";
            this.btn_patientCarePlans.UseVisualStyleBackColor = true;
            this.btn_patientCarePlans.Click += new System.EventHandler(this.btn_patientCarePlans_Click);
            // 
            // PatientSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 817);
            this.Controls.Add(this.btn_patientCarePlans);
            this.Controls.Add(this.btn_viewPatientHistory);
            this.Controls.Add(this.btn_getAllPatients);
            this.Controls.Add(this.comboBox_searchCategories);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.dataGridView_PatientList);
            this.Controls.Add(this.btn_editPatientInfo);
            this.Controls.Add(this.btn_deletePatient);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txtB_searchText);
            this.Name = "PatientSearchForm";
            this.Text = "Patient Search";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PatientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtB_searchText;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_deletePatient;
        private System.Windows.Forms.Button btn_editPatientInfo;
        private System.Windows.Forms.DataGridView dataGridView_PatientList;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.ComboBox comboBox_searchCategories;
        private System.Windows.Forms.Button btn_getAllPatients;
        private System.Windows.Forms.Button btn_viewPatientHistory;
        private System.Windows.Forms.Button btn_patientCarePlans;
    }
}