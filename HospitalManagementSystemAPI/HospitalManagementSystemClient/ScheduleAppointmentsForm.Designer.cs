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
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_patientId = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.numericUpDown_duration = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbl_searchTypeNote = new System.Windows.Forms.Label();
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
            // 
            // comboBox_appointmentStatus
            // 
            this.comboBox_appointmentStatus.FormattingEnabled = true;
            this.comboBox_appointmentStatus.Items.AddRange(new object[] {
            "Scheduled,",
            "InProgress,",
            "Completed,",
            "Cancelled,",
            "NoShow"});
            this.comboBox_appointmentStatus.Location = new System.Drawing.Point(192, 401);
            this.comboBox_appointmentStatus.Name = "comboBox_appointmentStatus";
            this.comboBox_appointmentStatus.Size = new System.Drawing.Size(121, 24);
            this.comboBox_appointmentStatus.TabIndex = 6;
            this.comboBox_appointmentStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Appointment Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(192, 167);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(176, 22);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(399, 35);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 10;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // txtB_patientSearch
            // 
            this.txtB_patientSearch.Location = new System.Drawing.Point(166, 36);
            this.txtB_patientSearch.Name = "txtB_patientSearch";
            this.txtB_patientSearch.Size = new System.Drawing.Size(217, 22);
            this.txtB_patientSearch.TabIndex = 11;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(572, 177);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(89, 23);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.Text = "Schedule";
            this.btn_Add.UseVisualStyleBackColor = true;
            // 
            // lbl_patientName
            // 
            this.lbl_patientName.AutoSize = true;
            this.lbl_patientName.Location = new System.Drawing.Point(132, 88);
            this.lbl_patientName.Name = "lbl_patientName";
            this.lbl_patientName.Size = new System.Drawing.Size(44, 16);
            this.lbl_patientName.TabIndex = 14;
            this.lbl_patientName.Text = "label2";
            this.lbl_patientName.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Patient Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Patient Id";
            // 
            // lbl_patientId
            // 
            this.lbl_patientId.AutoSize = true;
            this.lbl_patientId.Location = new System.Drawing.Point(132, 129);
            this.lbl_patientId.Name = "lbl_patientId";
            this.lbl_patientId.Size = new System.Drawing.Size(44, 16);
            this.lbl_patientId.TabIndex = 16;
            this.lbl_patientId.Text = "label5";
            this.lbl_patientId.Visible = false;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(572, 224);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(89, 23);
            this.btn_update.TabIndex = 18;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(572, 272);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(89, 23);
            this.btn_delete.TabIndex = 19;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_duration
            // 
            this.numericUpDown_duration.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_duration.Location = new System.Drawing.Point(192, 229);
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
            this.label6.Location = new System.Drawing.Point(24, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Duration";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 404);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Appointment Status";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 272);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Reason for Appointment";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(192, 274);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(272, 96);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            // 
            // lbl_searchTypeNote
            // 
            this.lbl_searchTypeNote.AutoSize = true;
            this.lbl_searchTypeNote.Location = new System.Drawing.Point(163, 15);
            this.lbl_searchTypeNote.Name = "lbl_searchTypeNote";
            this.lbl_searchTypeNote.Size = new System.Drawing.Size(94, 16);
            this.lbl_searchTypeNote.TabIndex = 28;
            this.lbl_searchTypeNote.Text = "Patient Search";
            // 
            // ScheduleAppointmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 491);
            this.Controls.Add(this.lbl_searchTypeNote);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDown_duration);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_patientId);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_patientId;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.NumericUpDown numericUpDown_duration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lbl_searchTypeNote;
    }
}