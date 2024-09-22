using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI;

namespace TaskManagerAPITests
{
    // This is the test class for equivalence partitioning and boundary value analysis
    // The business rules for task creation are: Task name not empty, should be unique, min 1 chars, max 20. Task value min 10 chars, max 100. Date must be a later date.
    public class TaskManagerPartBoundTests
    {
        private TaskManager taskManager = new TaskManager();

        [Theory]
        [InlineData("", "Task name cannot be empty. It must have at least 1 character.")] // Empty (invalid)
        [InlineData("T", null)] // 1 char (valid)
        [InlineData("TaskName", null)] // 8 chars (valid)
        [InlineData("12345678911234567891", null)] // 20 chars (valid)
        [InlineData("123456789112345678911", "Task name must not exceed 20 characters.")] // 21 characters (invalid)
        public void ValidateTask_ShouldValidateTaskNameLengthAndErrorMessage(string taskName, string expectedErrorMessage)
        {
            // Arrange
            var task = new TaskClass
            {
                TaskName = taskName,
                TaskValue = "Valid Task Value",
                Deadline = DateTime.Now.AddDays(1),
                IsCompleted = false
            };

            if (expectedErrorMessage != null)
            {
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => taskManager.ValidateTask(task));
                Assert.Equal(expectedErrorMessage, exception.Message);
            }
            else
            {
                // Act
                var expectedBool = true;
                var actualBool = taskManager.ValidateTask(task);
                // Assert
                Assert.Equal(expectedBool, actualBool);
            }
        }
    }
}
