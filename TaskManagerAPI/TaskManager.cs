namespace TaskManagerAPI
{
    public class TaskManager
    {
        private List<TaskClass> tasks;
        private int nextId = 1;
        //private FileIO fileIO;
        private DBManager _dbManager;

        public TaskManager()
        {
            //fileIO = new FileIO();
            //tasks = fileIO.Read_File() ?? new List<TaskClass>();
            //tasks = new List<TaskClass>();
            _dbManager = new DBManager();
            tasks = _dbManager.FindAllTasks();
        }

        // Create a new task
        public void CreateTask(string name, string value, DateTime deadline, bool isCompleted, string? category = null)
        {
            if (deadline <= DateTime.Now)
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
        public List<TaskClass> UpdateTask(string name, string newValue, DateTime newDeadline, bool isCompleted, string category)
        {
            // Find the task in the database
            var existingTask = _dbManager.FindTaskByName(name);

            if (existingTask != null)
            {
                // Create an updated task object
                var updatedTask = new TaskClass
                {
                    TaskName = name,  // Keep the same name
                    TaskValue = newValue,
                    Deadline = newDeadline,
                    IsCompleted = isCompleted,
                    Category = category
                };

                // Update the task in the database
                _dbManager.UpdateTaskByName(name, updatedTask);
                Console.WriteLine($"Task '{name}' updated.");
        }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }

            // Return the updated list of tasks
            return _dbManager.FindAllTasks();;
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
