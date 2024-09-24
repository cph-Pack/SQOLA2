namespace TaskManagerAPI
{
    public interface IDBManager
    {
        TaskClass FindTaskByName(string name);
        void InsertTask(TaskClass task);
        List<TaskClass> FindAllTasks();
        void UpdateTaskByName(string name, TaskClass task);
        void DeleteTaskByName(string name);
    }
}
