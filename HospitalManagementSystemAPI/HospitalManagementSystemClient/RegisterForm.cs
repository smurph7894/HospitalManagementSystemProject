﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalManagementSystemClient.Models;
using HospitalManagementSystemClient.Services;
using Newtonsoft.Json;

//NyamburaS


namespace HospitalManagementSystemClient
{
    public partial class RegisterForm : Form
    {
        //MongoDb service used to interact with 'userData' collection in HospitalManegemtnDB
        private readonly MongoDbService _mongoService;
        public RegisterForm()
        {
            InitializeComponent();

            //Initialize MongoDB connection
            _mongoService = new MongoDbService("HospitalManagementDB", "userData");

            //Bind the ComboBox to the Role Enum (Doctor, Nurse etc..) 
            comboBox_role.DataSource = Enum.GetNames(typeof(Role));
            comboBox_role.DropDownStyle = ComboBoxStyle.DropDownList; // restrict typing

        }

        //Register Button CLick Event
        private  void btn_register_Click(object sender, EventArgs e)
        {
            //Gathers and trims input fields 
            var username = txb_username.Text.Trim();
            var password = txb_password.Text;
            var fullName = txb_fullname.Text;
            var email = txb_email.Text;
            var roleStr = comboBox_role.SelectedItem.ToString(); //null-safe check

            //Field Validation: Prevents registration with any missing or blank fields
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(txb_phone.Text) ||
                string.IsNullOrWhiteSpace(txb_address.Text) ||
                string.IsNullOrEmpty(roleStr))
            {
                MessageBox.Show("Please fill in all fields before registering.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            //prevents duplicate usernames 
            if (_mongoService.UsernameExists(username))
            {
                MessageBox.Show("Username already exists. Please choose another.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //convert the selected item string back to the Role enum
            Role selectedRole = (Role)Enum.Parse(typeof(Role), comboBox_role.SelectedItem.ToString());

            var registrationDto = new
            {
                Username = txb_username.Text.Trim(),
                Password = txb_password.Text,
                Email = txb_email.Text.Trim(),
                Roles = new List<string> { selectedRole.ToString() },
                Profile = new
                {
                    FullName = txb_fullname.Text,
                    Phone = txb_phone.Text,
                    Address = txb_address.Text
                }
            };

            string json = JsonConvert.SerializeObject(registrationDto); // serialization

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.PostAsync("https://yourapiurl/api/users/register", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        string error = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show("Registration failed: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            this.Close(); //closes registration form
        }

    }
    
}
