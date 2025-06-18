using System.Collections.Generic;
using HospitalManagementSystemClient.Models;


namespace HospitalManagementSystemClient
{
    public static class PermissionHelper
    {
        public static List<Permission> GetPermissionsForRole(Role role)
        {
            List<Permission> permissions = new List<Permission>();

            switch (role)
            {
                case Role.Doctor:
                    permissions.Add(Permission.ViewMedicalHistory);
                    permissions.Add(Permission.ManageInventory);
                    permissions.Add(Permission.AccessReports);
                    permissions.Add(Permission.ViewVitals);
                    permissions.Add(Permission.ScheduleApp);
                    break;

                case Role.Nurse:
                    permissions.Add(Permission.ViewVitals);
                    permissions.Add(Permission.ScheduleApp);
                    break;

                case Role.AdministrativeStaff:
                    permissions.Add(Permission.ManageAppointments);
                    permissions.Add(Permission.ManageDepartments);
                    break;

                case Role.Administrator:
                    permissions.Add(Permission.ManageUsers);
                    permissions.Add(Permission.ManageStaff);
                    permissions.Add(Permission.AccessReports);
                    break;

                case Role.Patient:
                    permissions.Add(Permission.ScheduleApp);
                    permissions.Add(Permission.ViewMedicalHistory);
                    break;
            }

            return permissions;
        }
    }
}