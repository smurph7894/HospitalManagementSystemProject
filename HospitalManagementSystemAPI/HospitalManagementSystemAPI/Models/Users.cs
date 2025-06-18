using MongoDB.Bson.Serialization.Attributes;
using System.Security;
using System.Text.Json.Serialization;
//Nyambura Note: 
// This Users.cs model was originally created for use in the API project, 
// but due to framework compatibility limitations between the API (.NET Core) 
// and the Client (WinForms, .NET Framework), it could not be referenced directly.
//
// A separate, simplified version of the Users model was recreated in the Client project
// to enable user authentication via MongoDB. 
//
// This file can be deleted or retained only for reference or future integration 
// if both projects are unified under a compatible .NET framework.


namespace HospitalManagementSystemAPI.Models
{
    public enum Permission
    {
        ScheduleApp,
        ViewMedicalHistory,
        ManageInventory,
        AccessReports,
        ManageStaff,
        ViewVitals,
        ManageAdmissions,
        ManageAppointments,
        ManageBeds,
        ManageDepartments,
        ManageUsers,
    }

    public enum Role
    {
        Doctor,
        Nurse,
        AdmininistrativeStaff,
        Patient,
        Administrator
    }

    [Serializable]
    public class Users
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("Username"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Username { get; set; }

        [BsonElement("Email"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Email { get; set; }

        [BsonElement("Password"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Password { get; set; }


        [BsonElement("Roles"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public List<Role> Roles { get; set; }

        [BsonElement("Permissions"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public List<Permission> Permissions { get; set; }

        [BsonElement("Profile")]
        public Profile Profile { get; set; }

        [BsonElement("CreatedAt"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("UpdatedAt"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }

    [Serializable]
    public class Profile
    {
        [BsonElement("FullName"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string FullName { get; set; }

        [BsonElement("Phone"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Phone { get; set; }

        [BsonElement("Address"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Address { get; set; }
    }
}
