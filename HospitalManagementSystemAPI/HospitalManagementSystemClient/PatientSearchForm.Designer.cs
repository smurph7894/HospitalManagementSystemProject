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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_deletePatient = new System.Windows.Forms.Button();
            this.btn_editPatientInfo = new System.Windows.Forms.Button();
            this.dataGridView_PatientList = new System.Windows.Forms.DataGridView();
            this.btn_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PatientList)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(272, 22);
            this.textBox1.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(369, 63);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_deletePatient
            // 
            this.btn_deletePatient.Location = new System.Drawing.Point(798, 63);
            this.btn_deletePatient.Name = "btn_deletePatient";
            this.btn_deletePatient.Size = new System.Drawing.Size(119, 23);
            this.btn_deletePatient.TabIndex = 2;
            this.btn_deletePatient.Text = "Delete Patient";
            this.btn_deletePatient.UseVisualStyleBackColor = true;
            this.btn_deletePatient.Click += new System.EventHandler(this.btn_deletePatient_Click);
            // 
            // btn_editPatientInfo
            // 
            this.btn_editPatientInfo.Location = new System.Drawing.Point(661, 63);
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
            this.dataGridView_PatientList.Location = new System.Drawing.Point(27, 114);
            this.dataGridView_PatientList.Name = "dataGridView_PatientList";
            this.dataGridView_PatientList.RowHeadersWidth = 51;
            this.dataGridView_PatientList.RowTemplate.Height = 24;
            this.dataGridView_PatientList.Size = new System.Drawing.Size(963, 428);
            this.dataGridView_PatientList.TabIndex = 4;
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
            // PatientSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 585);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.dataGridView_PatientList);
            this.Controls.Add(this.btn_editPatientInfo);
            this.Controls.Add(this.btn_deletePatient);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.textBox1);
            this.Name = "PatientSearchForm";
            this.Text = "PatientSearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PatientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_deletePatient;
        private System.Windows.Forms.Button btn_editPatientInfo;
        private System.Windows.Forms.DataGridView dataGridView_PatientList;
        private System.Windows.Forms.Button btn_back;
    }
}