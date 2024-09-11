using MongoDB.Bson;
using MongoDB.Driver;

namespace TaskManagerAPI
{
    public class DBManager
    {
        private const string connectionUri = "mongodb://localhost:27017";
        private readonly MongoClient _client;

        public DBManager() 
        {
            _client = new MongoClient(connectionUri);
        }

        public List<TaskClass> FindAllTasks() 
        {
            // Creates a new client and connects to the server
            var database = _client.GetDatabase("SQ");
            var collection = database.GetCollection<TaskClass>("OLA2");

            var tasks = collection.Find(_ => true).ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (TaskClass task in tasks)
            {
                Console.WriteLine($"{task.Category}");
            }
            return tasks;
        }
        

    }
}
