using HospitalManagementSystemClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystemClient
{
    public partial class MedicalHistoryForm : Form
    {
        public MedicalHistoryForm(Users _loggedInUser, Patient patientInfo)
        {
            InitializeComponent();
        }
    }
}
