namespace TaskManagerAPI
{
    public class TaskManager
    {
        private List<TaskClass> tasks;
        private int nextId = 1;
        //private FileIO fileIO;
        private DBManager dbManager;

        public TaskManager()
        {
            //fileIO = new FileIO();
            //tasks = fileIO.Read_File() ?? new List<TaskClass>();
            //tasks = new List<TaskClass>();
            dbManager = new DBManager();
            tasks = dbManager.FindAllTasks();
        }

        // Create a new task
        public void CreateTask(string name, string value, DateOnly deadline, bool isCompleted, string? category = null)
        {
            if (deadline <= DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Deadline must be a future date");
            }
            //var task = new TaskClass(name, value, deadline, isCompleted);
            string taskCategory = string.IsNullOrWhiteSpace(category) ? "Default Category" : category;
            var task = new TaskClass()
            {
                TaskName = name,
                TaskValue = value,
                Category = taskCategory,
                Deadline = deadline,
                IsCompleted = isCompleted
            };
            tasks.Add(task);
            SaveTasks();
            Console.WriteLine($"Task '{name}' created.");
        }

        // Read a specific task by name
        public TaskClass GetTask(string name)
        {
            var task = tasks.FirstOrDefault(t => t.TaskName == name);
            if (task != null)
            {
                Console.WriteLine($"Task found: {task.TaskName} - {task.TaskValue} - Category: {task.Category} - Deadline: {task.Deadline} - Completed: {task.IsCompleted}");
            }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }
            return task;
        }

        // Read all tasks
        public List<TaskClass> GetAllTasks()
        {
            return tasks;
        }

        // Update an existing task by name
        public List<TaskClass> UpdateTask(string name, string newValue, DateOnly newDeadline, bool isCompleted, string category)
        {
            var task = tasks.FirstOrDefault(t => t.TaskName == name);
            if (task != null)
            {
                task.TaskValue = newValue;
                task.Deadline = newDeadline;
                task.IsCompleted = isCompleted;
                task.Category = category;
                Console.WriteLine($"Task '{name}' updated.");
            }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }
            return tasks;
        }

        // Delete a task by name
        public void DeleteTask(string name)
        {
            var task = tasks.FirstOrDefault(t => t.TaskName == name);
            if (task != null)
            {
                tasks.Remove(task);
                SaveTasks();
                Console.WriteLine($"Task '{name}' deleted.");
            }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }

        }

        private void SaveTasks()
        {
            try
            {
                //fileIO.Write_File(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }

        }
    }
}
