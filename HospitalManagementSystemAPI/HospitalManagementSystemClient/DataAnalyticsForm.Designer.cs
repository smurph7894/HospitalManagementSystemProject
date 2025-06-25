namespace HospitalManagementSystemClient
{
    partial class DataAnalyticsForm
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
            this.dgv_Report = new System.Windows.Forms.DataGridView();
            this.btn_PatientVisits = new System.Windows.Forms.Button();
            this.btn_CommonAilments = new System.Windows.Forms.Button();
            this.btn_MedicationUsage = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Report)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Report
            // 
            this.dgv_Report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Report.Location = new System.Drawing.Point(61, 205);
            this.dgv_Report.Name = "dgv_Report";
            this.dgv_Report.RowHeadersWidth = 51;
            this.dgv_Report.RowTemplate.Height = 24;
            this.dgv_Report.Size = new System.Drawing.Size(693, 220);
            this.dgv_Report.TabIndex = 4;
            // 
            // btn_PatientVisits
            // 
            this.btn_PatientVisits.Location = new System.Drawing.Point(340, 134);
            this.btn_PatientVisits.Name = "btn_PatientVisits";
            this.btn_PatientVisits.Size = new System.Drawing.Size(142, 48);
            this.btn_PatientVisits.TabIndex = 5;
            this.btn_PatientVisits.Text = "Patient Visits";
            this.btn_PatientVisits.UseVisualStyleBackColor = true;
            this.btn_PatientVisits.Click += new System.EventHandler(this.btn_PatientVisits_Click);
            // 
            // btn_CommonAilments
            // 
            this.btn_CommonAilments.Location = new System.Drawing.Point(61, 134);
            this.btn_CommonAilments.Name = "btn_CommonAilments";
            this.btn_CommonAilments.Size = new System.Drawing.Size(141, 48);
            this.btn_CommonAilments.TabIndex = 5;
            this.btn_CommonAilments.Text = "Common Ailments";
            this.btn_CommonAilments.UseVisualStyleBackColor = true;
            this.btn_CommonAilments.Click += new System.EventHandler(this.btn_CommonAilments_Click);
            // 
            // btn_MedicationUsage
            // 
            this.btn_MedicationUsage.Location = new System.Drawing.Point(613, 134);
            this.btn_MedicationUsage.Name = "btn_MedicationUsage";
            this.btn_MedicationUsage.Size = new System.Drawing.Size(141, 48);
            this.btn_MedicationUsage.TabIndex = 5;
            this.btn_MedicationUsage.Text = "Medication Usage";
            this.btn_MedicationUsage.UseVisualStyleBackColor = true;
            this.btn_MedicationUsage.Click += new System.EventHandler(this.btn_MedicationUsage_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(187, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data Analytics: Generate Reports";
            // 
            // DataAnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_MedicationUsage);
            this.Controls.Add(this.btn_CommonAilments);
            this.Controls.Add(this.btn_PatientVisits);
            this.Controls.Add(this.dgv_Report);
            this.Name = "DataAnalyticsForm";
            this.Text = "DataAnalyticsForm";
            this.Load += new System.EventHandler(this.DataAnalyticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Report)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Report;
        private System.Windows.Forms.Button btn_PatientVisits;
        private System.Windows.Forms.Button btn_CommonAilments;
        private System.Windows.Forms.Button btn_MedicationUsage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}