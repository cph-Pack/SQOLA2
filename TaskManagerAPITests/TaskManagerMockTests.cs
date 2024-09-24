using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI;

namespace TaskManagerAPITests
{
    public class TaskManagerMockTests
    {
        [Fact]
        public void CreateTask_TaskAlreadyExists_ThrowsArgumentException()
        {
            // Arrange: Mock interfacet IDBManager i stedet for den konkrete DBManager
            var mockDbManager = new Mock<IDBManager>();
            var taskManager = new TaskManager(mockDbManager.Object); // Injicer mock af IDBManager

            var task = new TaskClass
            {
                TaskName = "Task with letters",
                Deadline = DateTime.Now.AddDays(1),
                Category = "Work",
                TaskValue = "Medium with 10+ letters"
            };

            // Setup: Simuler at en task med samme navn allerede findes i databasen
            mockDbManager.Setup(db => db.FindTaskByName(It.Is<string>(name => name == task.TaskName)))
                         .Returns(task); // Mock returnerer en eksisterende task

            // Act & Assert: Verificer at der kastes en ArgumentException, når task allerede eksisterer
            var exception = Assert.Throws<ArgumentException>(() => taskManager.CreateTask(task));
            Assert.Equal("Task already exists", exception.Message);
        }
    }
}
