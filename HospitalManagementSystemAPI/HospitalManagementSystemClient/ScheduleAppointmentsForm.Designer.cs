using System;

namespace HospitalManagementSystemClient
{
    partial class ScheduleAppointmentsForm
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
            this.btn_Dashboard = new System.Windows.Forms.Button();
            this.comboBox_appointmentStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_search = new System.Windows.Forms.Button();
            this.txtB_patientSearch = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.lbl_patientName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_patientIdTitle = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.numericUpDown_duration = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbl_searchTypeNote = new System.Windows.Forms.Label();
            this.txtB_patientId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_duration)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Dashboard
            // 
            this.btn_Dashboard.Location = new System.Drawing.Point(12, 12);
            this.btn_Dashboard.Name = "btn_Dashboard";
            this.btn_Dashboard.Size = new System.Drawing.Size(100, 23);
            this.btn_Dashboard.TabIndex = 5;
            this.btn_Dashboard.Text = "Dashboard";
            this.btn_Dashboard.UseVisualStyleBackColor = true;
            this.btn_Dashboard.Click += new System.EventHandler(this.btn_Dashboard_Click);
            // 
            // comboBox_appointmentStatus
            // 
            this.comboBox_appointmentStatus.FormattingEnabled = true;
            this.comboBox_appointmentStatus.Items.AddRange(new object[] {
            "Scheduled",
            "InProgress",
            "Completed",
            "Cancelled",
            "NoShow"});
            this.comboBox_appointmentStatus.Location = new System.Drawing.Point(192, 437);
            this.comboBox_appointmentStatus.Name = "comboBox_appointmentStatus";
            this.comboBox_appointmentStatus.Size = new System.Drawing.Size(121, 24);
            this.comboBox_appointmentStatus.TabIndex = 6;
            this.comboBox_appointmentStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Appointment Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(192, 203);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(176, 22);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(425, 57);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 10;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txtB_patientSearch
            // 
            this.txtB_patientSearch.Location = new System.Drawing.Point(192, 58);
            this.txtB_patientSearch.Name = "txtB_patientSearch";
            this.txtB_patientSearch.Size = new System.Drawing.Size(217, 22);
            this.txtB_patientSearch.TabIndex = 11;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(572, 213);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(89, 23);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.Text = "Schedule";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // lbl_patientName
            // 
            this.lbl_patientName.AutoSize = true;
            this.lbl_patientName.Location = new System.Drawing.Point(133, 122);
            this.lbl_patientName.Name = "lbl_patientName";
            this.lbl_patientName.Size = new System.Drawing.Size(0, 16);
            this.lbl_patientName.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Patient Name";
            // 
            // lbl_patientIdTitle
            // 
            this.lbl_patientIdTitle.AutoSize = true;
            this.lbl_patientIdTitle.Location = new System.Drawing.Point(26, 152);
            this.lbl_patientIdTitle.Name = "lbl_patientIdTitle";
            this.lbl_patientIdTitle.Size = new System.Drawing.Size(62, 16);
            this.lbl_patientIdTitle.TabIndex = 17;
            this.lbl_patientIdTitle.Text = "Patient Id";
            this.lbl_patientIdTitle.Visible = false;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(572, 260);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(89, 23);
            this.btn_update.TabIndex = 18;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(572, 308);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(89, 23);
            this.btn_delete.TabIndex = 19;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // numericUpDown_duration
            // 
            this.numericUpDown_duration.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_duration.Location = new System.Drawing.Point(192, 243);
            this.numericUpDown_duration.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDown_duration.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_duration.Name = "numericUpDown_duration";
            this.numericUpDown_duration.Size = new System.Drawing.Size(62, 22);
            this.numericUpDown_duration.TabIndex = 21;
            this.numericUpDown_duration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Duration";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(24, 440);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(122, 16);
            this.lbl_status.TabIndex = 24;
            this.lbl_status.Text = "Appointment Status";
            this.lbl_status.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Reason for Appointment";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(192, 310);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(272, 96);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            // 
            // lbl_searchTypeNote
            // 
            this.lbl_searchTypeNote.AutoSize = true;
            this.lbl_searchTypeNote.Location = new System.Drawing.Point(189, 37);
            this.lbl_searchTypeNote.Name = "lbl_searchTypeNote";
            this.lbl_searchTypeNote.Size = new System.Drawing.Size(160, 16);
            this.lbl_searchTypeNote.TabIndex = 28;
            this.lbl_searchTypeNote.Text = "Search by Appointment Id";
            // 
            // txtB_patientId
            // 
            this.txtB_patientId.Location = new System.Drawing.Point(136, 152);
            this.txtB_patientId.Name = "txtB_patientId";
            this.txtB_patientId.Size = new System.Drawing.Size(104, 22);
            this.txtB_patientId.TabIndex = 29;
            this.txtB_patientId.Visible = false;
            // 
            // ScheduleAppointmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 491);
            this.Controls.Add(this.txtB_patientId);
            this.Controls.Add(this.lbl_searchTypeNote);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDown_duration);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.lbl_patientIdTitle);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbl_patientName);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.txtB_patientSearch);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_appointmentStatus);
            this.Controls.Add(this.btn_Dashboard);
            this.Name = "ScheduleAppointmentsForm";
            this.Text = "Schedule Appointments";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_duration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Dashboard;
        private System.Windows.Forms.ComboBox comboBox_appointmentStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txtB_patientSearch;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label lbl_patientName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_patientIdTitle;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.NumericUpDown numericUpDown_duration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lbl_searchTypeNote;
        private System.Windows.Forms.TextBox txtB_patientId;
    }
}