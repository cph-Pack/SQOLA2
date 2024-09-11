namespace TaskManagerAPI
{
    public class TaskClass
    {
        public string TaskName { get; set; }
        public string TaskValue { get; set; }
        public DateOnly Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public string Category { get; set; }
    }
}
