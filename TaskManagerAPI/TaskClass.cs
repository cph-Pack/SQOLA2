using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagerAPI
{
    public class TaskClass
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("taskName")]
        public string TaskName { get; set; }

        [BsonElement("taskValue")]  
        public string TaskValue { get; set; }


        [BsonElement("deadline")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Deadline { get; set; }  

        [BsonElement("isCompleted")]  
        public bool IsCompleted { get; set; }

        [BsonElement("category")]  
        public string Category { get; set; }
    }
}
