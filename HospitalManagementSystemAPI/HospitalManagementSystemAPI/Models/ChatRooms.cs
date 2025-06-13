using MongoDB.Bson.Serialization.Attributes;

namespace HospitalManagementSystemAPI.Models
{
    [Serializable]
    public class ChatRooms
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ChatRoomId { get; set; }

        [BsonElement("Participants"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public List<string> Participants { get; set; }

        [BsonElement("CreatedAt"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
