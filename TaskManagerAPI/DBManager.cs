using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TaskManagerAPI
{
    public class DBManager
    {
        private const string connectionUri = "mongodb://localhost:27017";
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TaskClass> _collection;

        public DBManager()
        {
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase("SQ");
            _collection = _database.GetCollection<TaskClass>("OLA2");
        }

        public void InsertTask(TaskClass task)
        {
            _collection.InsertOne(task);
            Console.WriteLine($"Task {task.TaskName} inserted successfully.");
        }

        public List<TaskClass> FindAllTasks()
        {
            var tasks = _collection.Find(_ => true).ToList();
            return tasks;
        }

        public TaskClass FindTaskByName(string name)
        {
            var filter = Builders<TaskClass>.Filter.Eq(t => t.TaskName, name);
            var task = _collection.Find(filter).FirstOrDefault();
            return task;
        }

        public void UpdateTaskByName(string name, TaskClass updatedTask)
        {
            var filter = Builders<TaskClass>.Filter.Eq(t => t.TaskName, name);
            var update = Builders<TaskClass>.Update
                            .Set(t => t.TaskName, updatedTask.TaskName)
                            .Set(t => t.TaskValue, updatedTask.TaskValue)
                            .Set(t => t.Deadline, updatedTask.Deadline)
                            .Set(t => t.IsCompleted, updatedTask.IsCompleted)
                            .Set(t => t.Category, updatedTask.Category);

            var result = _collection.UpdateOne(filter, update);
            if (result.ModifiedCount > 0)
            {
                Console.WriteLine($"Task {name} updated successfully.");
            }
            else
            {
                Console.WriteLine($"Task {name} not found or no changes made.");
            }
        }

        public void DeleteTaskByName(string name)
        {
            var filter = Builders<TaskClass>.Filter.Eq(t => t.TaskName, name);
            var result = _collection.DeleteOne(filter);
            if (result.DeletedCount > 0)
            {
                Console.WriteLine($"Task {name} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Task {name} not found.");
            }
        }
    }
}
