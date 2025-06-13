using MongoDB.Bson.Serialization.Attributes;
using System.Security;
using System.Text.Json.Serialization;

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

        [BsonElement("Password"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Password { get; set; }

        [BsonElement("Roles"), BsonRepresentation(MongoDB.Bson.BsonType.Array)]
        public List<Role> Roles { get; set; }

        [BsonElement("Permissions"), BsonRepresentation(MongoDB.Bson.BsonType.Array)]
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
