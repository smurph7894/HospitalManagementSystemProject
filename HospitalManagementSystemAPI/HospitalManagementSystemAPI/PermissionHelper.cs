using System.Collections.Generic;
using HospitalManagementSystemAPI.Models;


namespace HospitalManagementSystemAPI
{
    public static class PermissionHelper
    {
        public static List<Permission> GetPermissionsForRole(Role role)
        {
            var permissions = new List<Permission>();

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

                case Role.Staff:
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

        //Aggregate permissions from multiple roles
        public static List<Permission> GetPermissionsForRoles(List<Role> roles)
        {
            var permissions = new HashSet<Permission>();
            foreach (var role in roles)
            {
                var rolePerms = GetPermissionsForRole(role);
                foreach (var perm in rolePerms)
                {
                    permissions.Add(perm);
                }
            }
            return new List<Permission>(permissions);
        }
    }
}
