using MongoDB.Bson.Serialization.Attributes;

namespace HospitalManagementSystemAPI.Models
{
    [Serializable]
    public class ChatMessages
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ChatRoomId { get; set; }

        [BsonElement("RoomId"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string RoomId { get; set; }

        [BsonElement("SenderId"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string SenderId { get; set; }

        [BsonElement("Message"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Message { get; set; }

        [BsonElement("SentAt"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime SentAt { get; set; }
    }
}
