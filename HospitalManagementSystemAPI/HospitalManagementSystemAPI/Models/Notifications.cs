

using MongoDB.Bson.Serialization.Attributes;

namespace HospitalManagementSystemAPI.Models
{
    public enum Type
    {
        AppointmentChanged,
        AppointmentCancelled,
        AppointmentCreated,
        AdmissionCreated,
        AdmissionUpdated,
        AdmissionCancelled,
        BedAssigned,
        BedReleased,
        InventoryItemAdded,
        InventoryItemUpdated,
        InventoryItemRemoved,
        MedicalHistoryUpdated,
        ReportAdded,
        ReportUpdated,
        StaffAdded,
        StaffUpdated,
        StaffRemoved,
        PatientAdded,
        PatientUpdated,
        PatientRemoved,
        VitalSignsUpdated,
        GeneralNotification,
        Message
    }

    [Serializable]
    public class Notifications
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string NotificationsId { get; set; }

        [BsonElement("UserId"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("Type"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Type Type { get; set; }

        [BsonElement("Payload")]
        public object Payload { get; set; }

        [BsonElement("isRead"), BsonRepresentation(MongoDB.Bson.BsonType.Boolean)]
        public bool isRead { get; set; }

        [BsonElement("CreatedAt"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
