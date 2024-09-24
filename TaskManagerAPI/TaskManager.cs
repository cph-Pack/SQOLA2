using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace TaskManagerAPI
{
    //[ExcludeFromCodeCoverage]
    public class TaskManager
    {
        private DBManager _dbManager;

        public TaskManager()
        {
            _dbManager = new DBManager();
        }

        // Create a new task
        public void CreateTask(TaskClass task)
        {
            if (!TaskIsValid(task))
            {
                throw new ArgumentException("Task is invalid");
            }
            if (!UniqueTask(task.TaskName))
            {
                throw new ArgumentException("Task already exists");
            }
            _dbManager.InsertTask(task);
        }

        // Read a specific task by name
        public TaskClass GetTask(string name)
        {
            var task = _dbManager.FindTaskByName(name);
            if (task != null)
            {
                return task;
            }
            else
            {
                throw new ArgumentException("Could not find a task with that name");
            }
        }

        // Read all tasks
        public List<TaskClass> GetAllTasks()
        {
            var tasks = _dbManager.FindAllTasks();
            if (tasks.Count > 0)
            {
                return tasks;
            }
            else
            {
                throw new ArgumentException("There is no tasks in the DB...");
            }
        }

        // Update an existing task by name
        public List<TaskClass> UpdateTask(TaskClass task, string name)
        {
            if (!TaskIsValid(task))
            {
                throw new ArgumentException("Task is invalid");
            }

            var existingTask = _dbManager.FindTaskByName(name);
            if (existingTask != null)
            {
                _dbManager.UpdateTaskByName(name, task);
                Console.WriteLine($"Task '{name}' updated.");
            }
            else
            {
                throw new ArgumentException("Could not find a task with that name");
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
            }
            else
            {
                throw new ArgumentException("Could not find a task with that name");
            }

        }

        // The business rules for task creation are: Task name not empty, should be unique, min 1 chars, max 20. Task value min 10 chars, max 100. Date must be a later date.
        public bool TaskIsValid(TaskClass task)
        {
            if (string.IsNullOrEmpty(task.TaskName))
            {
                throw new ArgumentException("Task name cannot be empty. It must have at least 1 character.");
            }

            if (task.TaskName.Length > 20)
            {
                throw new ArgumentException("Task name must not exceed 20 characters.");
            }

            if (string.IsNullOrEmpty(task.TaskValue))
            {
                throw new ArgumentException("Task value cannot be empty.");
            }

            if (task.TaskValue.Length < 10)
            {
                throw new ArgumentException("Task value must have at least 10 characters.");
            }

            if (task.TaskValue.Length > 100)
            {
                throw new ArgumentException("Task value must not exceed 100 characters.");
            }

            if (task.Deadline <= DateTime.Now)
            {
                throw new ArgumentException("Task date must be a future date.");
            }
            return true;
        }

        public bool UniqueTask(string name)
        {
            var task = _dbManager.FindTaskByName(name);
            if (task == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
