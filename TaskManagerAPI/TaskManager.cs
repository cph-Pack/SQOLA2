using System.Diagnostics.CodeAnalysis;

namespace TaskManagerAPI
{
    [ExcludeFromCodeCoverage]
    public class TaskManager
    {
        private DBManager _dbManager;

        public TaskManager()
        {
            _dbManager = new DBManager();
        }

        // Create a new task
        public void CreateTask(string name, string value, DateTime deadline, bool isCompleted, string? category = null)
        {
            if (deadline <= DateTime.Now)
            {
                throw new ArgumentException("Deadline must be a future date");
            }

            string taskCategory = string.IsNullOrWhiteSpace(category) ? "Default Category" : category;
            var task = new TaskClass()
            {
                TaskName = name,
                TaskValue = value,
                Category = taskCategory,
                Deadline = deadline,
                IsCompleted = isCompleted
            };
            _dbManager.InsertTask(task);
            Console.WriteLine($"Task '{name}' created.");
        }

        // Read a specific task by name
        public TaskClass GetTask(string name)
        {
            var task = _dbManager.FindTaskByName(name);
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
            return _dbManager.FindAllTasks();
        }

        // Update an existing task by name
        public List<TaskClass> UpdateTask(string name, string newValue, DateTime newDeadline, bool isCompleted, string category)
        {
            var existingTask = _dbManager.FindTaskByName(name);

            if (existingTask != null)
            {
                var updatedTask = new TaskClass
                {
                    TaskName = name,  
                    TaskValue = newValue,
                    Deadline = newDeadline,
                    IsCompleted = isCompleted,
                    Category = category
                };

                _dbManager.UpdateTaskByName(name, updatedTask);
                Console.WriteLine($"Task '{name}' updated.");
        }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }

            return _dbManager.FindAllTasks();;
        }



        // Delete a task by name
        public void DeleteTask(string name)
        {
            var existingTask = _dbManager.FindTaskByName(name);
            if (existingTask != null)
            {
                _dbManager.DeleteTaskByName(name);
                Console.WriteLine($"Task '{name}' deleted.");
            }
            else
            {
                Console.WriteLine($"Task with name '{name}' not found.");
            }

        }
    }
}
