using System.Xml.Linq;
using TaskManagerAPI;
using static MongoDB.Driver.WriteConcern;

namespace TaskManagerAPITests
{
    public class TaskManagerAPITest
    {
        [Fact]
        public void GetTask_ExistingTask_Success()
        {
            // arrange
            TaskManager taskManager = new TaskManager();
            string name = "Changed by API";

            // act
            var actual = taskManager.GetTask(name);

            // assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void GetAllTasks()
        {
            // arrange
            TaskManager taskManager = new TaskManager();

            // act
            var tasks = taskManager.GetAllTasks();

            // assert
            Assert.NotNull(tasks);
        }

        [Fact]
        public void UpdateTask_ExistingTask_Success()
        {
            // arrange
            TaskManager taskManager = new TaskManager();
            string name = "Changed by API";
            string newValue = "high prio";
            DateTime newDeadline = new DateTime();
            bool isCompleted = true;
            string category = "coding";
            var updatedTask = new TaskClass
            {
                TaskName = name,
                TaskValue = newValue,
                Deadline = newDeadline,
                IsCompleted = isCompleted,
                Category = category
            };
            List<TaskClass> expected = new List<TaskClass>();
            expected.Add(updatedTask);

            // act
            /**
            var actual = taskManager.UpdateTask(name,newValue,newDeadline,isCompleted,category);

            // assert
            Assert.Equal(actual[0].TaskName, expected[0].TaskName);
            Assert.Equal(actual[0].TaskValue, expected[0].TaskValue);
            Assert.Equal(actual[0].Deadline, expected[0].Deadline);
            Assert.Equal(actual[0].IsCompleted, expected[0].IsCompleted);
            Assert.Equal(actual[0].Category, expected[0].Category);**/
        }
    }
}