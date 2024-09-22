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
        [InlineData(null, "Task name cannot be empty. It must have at least 1 character.")] // Null (invalid)
        [InlineData("", "Task name cannot be empty. It must have at least 1 character.")] // Empty (invalid)
        [InlineData("T", null)] // 1 char (valid)
        [InlineData("TaskName", null)] // 8 chars (valid)
        [InlineData("12345678901234567890", null)] // 20 chars (valid)
        [InlineData("123456789012345678901", "Task name must not exceed 20 characters.")] // 21 characters (invalid)
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

        [Theory]
        [InlineData(null, "Task value cannot be empty.")] // Null (invalid)
        [InlineData("", "Task value cannot be empty.")] // Empty (invalid)
        [InlineData("123456789", "Task value must have at least 10 characters.")] // 9 chars (invalid)
        [InlineData("1234567890", null)] // 10 chars (valid)
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula.", null)] // 80 characters (valid)
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m", null)] // 100 characters (valid)
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean ma", "Task value must not exceed 100 characters.")] // 101 chars (invalid)
        public void CreateTask_ShouldValidateTaskValueLengthAndErrorMessage(string taskValue, string expectedErrorMessage)
        {
            // Arrange
            var task = new TaskClass
            {
                TaskName = "TaskName",
                TaskValue = taskValue,
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

        [Theory]
        [InlineData(-1, "Task date must be a future date.")] // Yesterday (invalid)
        [InlineData(0, "Task date must be a future date.")] // Today (invalid)
        [InlineData(1, null)] // Tomorrow (valid)
        public void CreateTask_ShouldValidateDeadlineAndErrorMessage(int daysFromToday, string expectedErrorMessage)
        {
            // Arrange
            var task = new TaskClass
            {
                TaskName = "TaskName",
                TaskValue = "Valid Task Value",
                Deadline = DateTime.Now.AddDays(daysFromToday),
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
