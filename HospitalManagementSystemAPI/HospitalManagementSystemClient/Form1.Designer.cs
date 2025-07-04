﻿namespace HospitalManagementSystemClient
{
    partial class Form1
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
            this.txtB_message = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB_user = new System.Windows.Forms.TextBox();
            this.btn_Emergency = new System.Windows.Forms.Button();
            this.btn_BedUpdate = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_signout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtB_message
            // 
            this.txtB_message.Location = new System.Drawing.Point(318, 69);
            this.txtB_message.Name = "txtB_message";
            this.txtB_message.Size = new System.Drawing.Size(100, 22);
            this.txtB_message.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(53, 130);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(355, 292);
            this.listBox.TabIndex = 2;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(453, 68);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "User";
            // 
            // txtB_user
            // 
            this.txtB_user.Location = new System.Drawing.Point(77, 70);
            this.txtB_user.Name = "txtB_user";
            this.txtB_user.Size = new System.Drawing.Size(100, 22);
            this.txtB_user.TabIndex = 4;
            // 
            // btn_Emergency
            // 
            this.btn_Emergency.Location = new System.Drawing.Point(469, 143);
            this.btn_Emergency.Name = "btn_Emergency";
            this.btn_Emergency.Size = new System.Drawing.Size(207, 38);
            this.btn_Emergency.TabIndex = 6;
            this.btn_Emergency.Text = "Send Emergency Notification";
            this.btn_Emergency.UseVisualStyleBackColor = true;
            this.btn_Emergency.Click += new System.EventHandler(this.btn_Emergency_Click);
            // 
            // btn_BedUpdate
            // 
            this.btn_BedUpdate.Location = new System.Drawing.Point(472, 209);
            this.btn_BedUpdate.Name = "btn_BedUpdate";
            this.btn_BedUpdate.Size = new System.Drawing.Size(204, 34);
            this.btn_BedUpdate.TabIndex = 7;
            this.btn_BedUpdate.Text = "Bed Update Notification";
            this.btn_BedUpdate.UseVisualStyleBackColor = true;
            this.btn_BedUpdate.Click += new System.EventHandler(this.btn_BedUpdate_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(593, 32);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(101, 26);
            this.btn_back.TabIndex = 8;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_signout
            // 
            this.btn_signout.Location = new System.Drawing.Point(596, 76);
            this.btn_signout.Name = "btn_signout";
            this.btn_signout.Size = new System.Drawing.Size(97, 32);
            this.btn_signout.TabIndex = 9;
            this.btn_signout.Text = "Sign Out";
            this.btn_signout.UseVisualStyleBackColor = true;
            this.btn_signout.Click += new System.EventHandler(this.btn_signout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_signout);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_BedUpdate);
            this.Controls.Add(this.btn_Emergency);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtB_user);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtB_message);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtB_message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtB_user;
        private System.Windows.Forms.Button btn_Emergency;
        private System.Windows.Forms.Button btn_BedUpdate;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_signout;
    }
}

