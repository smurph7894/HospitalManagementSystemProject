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
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Report)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Report
            // 
            this.dgv_Report.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Report.Location = new System.Drawing.Point(81, 251);
            this.dgv_Report.Name = "dgv_Report";
            this.dgv_Report.RowHeadersWidth = 51;
            this.dgv_Report.RowTemplate.Height = 24;
            this.dgv_Report.Size = new System.Drawing.Size(846, 220);
            this.dgv_Report.TabIndex = 4;
            // 
            // btn_PatientVisits
            // 
            this.btn_PatientVisits.Location = new System.Drawing.Point(442, 185);
            this.btn_PatientVisits.Name = "btn_PatientVisits";
            this.btn_PatientVisits.Size = new System.Drawing.Size(142, 48);
            this.btn_PatientVisits.TabIndex = 5;
            this.btn_PatientVisits.Text = "Patient Visits";
            this.btn_PatientVisits.UseVisualStyleBackColor = true;
            this.btn_PatientVisits.Click += new System.EventHandler(this.btn_PatientVisits_Click);
            // 
            // btn_CommonAilments
            // 
            this.btn_CommonAilments.Location = new System.Drawing.Point(81, 185);
            this.btn_CommonAilments.Name = "btn_CommonAilments";
            this.btn_CommonAilments.Size = new System.Drawing.Size(141, 48);
            this.btn_CommonAilments.TabIndex = 5;
            this.btn_CommonAilments.Text = "Common Ailments";
            this.btn_CommonAilments.UseVisualStyleBackColor = true;
            this.btn_CommonAilments.Click += new System.EventHandler(this.btn_CommonAilments_Click);
            // 
            // btn_MedicationUsage
            // 
            this.btn_MedicationUsage.Location = new System.Drawing.Point(773, 185);
            this.btn_MedicationUsage.Name = "btn_MedicationUsage";
            this.btn_MedicationUsage.Size = new System.Drawing.Size(141, 48);
            this.btn_MedicationUsage.TabIndex = 5;
            this.btn_MedicationUsage.Text = "Medication Usage";
            this.btn_MedicationUsage.UseVisualStyleBackColor = true;
            this.btn_MedicationUsage.Click += new System.EventHandler(this.btn_MedicationUsage_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data Analytics: Generate Reports";
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Location = new System.Drawing.Point(411, 111);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_start.TabIndex = 8;
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Location = new System.Drawing.Point(411, 152);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_end.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "End";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Visits Between Dates";
            // 
            // DataAnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 574);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.dateTimePicker_start);
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
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}