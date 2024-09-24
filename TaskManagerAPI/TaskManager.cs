using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

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

        public bool TaskIsValid(TaskClass task)
        {
            bool isResultGood = true;

            if (task.Deadline <= DateTime.Now)
            {
                isResultGood = false;
            }
            if (string.IsNullOrEmpty(task.Category))
            {
                isResultGood = false;
            }
            if (task.TaskName.Length > 20)
            {
                isResultGood = false;
            }
            if (task.TaskValue.Length > 20)
            {
                isResultGood = false;
            }

            return isResultGood;

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
