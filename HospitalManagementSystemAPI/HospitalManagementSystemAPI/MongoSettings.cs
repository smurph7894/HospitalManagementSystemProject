using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HospitalManagementSystemAPI
{
    public class MongoSettings 
    { 
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; } 
    }        
}
