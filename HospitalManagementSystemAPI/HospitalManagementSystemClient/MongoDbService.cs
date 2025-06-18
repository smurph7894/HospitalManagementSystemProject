using MongoDB.Driver;
using System.Configuration;
using HospitalManagementSystemClient.Models; // For reading app.config

namespace HospitalManagementSystemClient.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Users> _usersCollection;

        public MongoDbService(string databseName, string collectionName)
        {
           
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("HospitalManagementDB");
            _usersCollection = database.GetCollection<Users>("userData");
        }

        public Users FindUserByUsername(string username)
        {
            return _usersCollection.Find(u => u.Username == username).FirstOrDefault();
        }


        public void RegisterUser(Users user)
        {
            _usersCollection.InsertOne(user);
        }

        public bool UsernameExists(string username)
        {
            return _usersCollection.Find(u => u.Username == username).Any();
        }
    }

}
