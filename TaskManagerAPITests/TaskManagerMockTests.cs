using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI;
using TaskManagerAPI.Controllers;

namespace TaskManagerAPITests
{
    public class TaskManagerMockTests
    {
        [Fact]
        public void CreateTaskAlreadyExists()
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
        [Fact]
        public void CreateUniqueTaska()
        {
            // Arrange: Mock IDBManager
            var mockDbManager = new Mock<IDBManager>();
            var taskManager = new TaskManager(mockDbManager.Object);  // Injicer mock

            var task = new TaskClass
            {
                TaskName = "NewTask",
                Deadline = DateTime.Now.AddDays(1),
                Category = "TestCategory",
                TaskValue = "TestValue1"
            };

            // Setup: Simuler at task ikke findes i databasen
            mockDbManager.Setup(db => db.FindTaskByName(It.IsAny<string>())).Returns((TaskClass)null);

            // Setup: Simuler indsættelse af task i databasen
            mockDbManager.Setup(db => db.InsertTask(It.IsAny<TaskClass>()));

            // Act: Opret task
            taskManager.CreateTask(task);

            // Assert: Verificer at task blev indsat i databasen
            mockDbManager.Verify(db => db.InsertTask(It.IsAny<TaskClass>()), Times.Once);
        }

        [Fact]
        public void GetExistingTask()
        {
            // Arrange: Mock IDBManager
            var mockDbManager = new Mock<IDBManager>();
            var taskManager = new TaskManager(mockDbManager.Object);  // Injicer mock

            var task = new TaskClass
            {
                TaskName = "Task1",
                TaskValue = "TestValue1",
                Deadline = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Category = "TestCategory1"
            };

            // Setup: Simuler at finde en task i databasen
            mockDbManager.Setup(db => db.FindTaskByName(It.IsAny<string>())).Returns(task);

            // Act: Hent task
            var result = taskManager.GetTask("Task1");

            // Assert: Verificer at den rigtige task blev returneret
            Assert.Equal(task.TaskName, result.TaskName);
            Assert.Equal(task.TaskValue, result.TaskValue);
        }

        [Fact]
        public void UpdateTask()
        {
            // Arrange: Mock IDBManager
            var mockDbManager = new Mock<IDBManager>();
            var taskManager = new TaskManager(mockDbManager.Object);  // Injicer mock

            var existingTask = new TaskClass
            {
                TaskName = "Task1",
                TaskValue = "OldValue",
                Deadline = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Category = "OldCategory"
            };

            var updatedTask = new TaskClass
            {
                TaskName = "Task1",
                TaskValue = "OldValueover 10characters",
                Deadline = DateTime.Now.AddDays(3),
                IsCompleted = true,
                Category = "NewCategory"
            };

            // Setup: Mock returnerer en eksisterende task
            mockDbManager.Setup(db => db.FindTaskByName("Task1")).Returns(existingTask);

            // Setup: Simuler task update i databasen
            mockDbManager.Setup(db => db.UpdateTaskByName(It.IsAny<string>(), It.IsAny<TaskClass>()));

            // Act: Opdater task
            taskManager.UpdateTask(updatedTask, "Task1");

            // Assert: Verificer at task blev opdateret
            mockDbManager.Verify(db => db.UpdateTaskByName("Task1", updatedTask), Times.Once);
        }

        [Fact]
        public void DeleteTask()
        {
            // Arrange: Mock IDBManager
            var mockDbManager = new Mock<IDBManager>();
            var taskManager = new TaskManager(mockDbManager.Object);  // Injicer mock

            var task = new TaskClass
            {
                TaskName = "Task1"
            };

            // Setup: Simuler at task findes i databasen
            mockDbManager.Setup(db => db.FindTaskByName("Task1")).Returns(task);

            // Setup: Simuler sletning af task i databasen
            mockDbManager.Setup(db => db.DeleteTaskByName("Task1"));

            // Act: Slet task
            taskManager.DeleteTask("Task1");

            // Assert: Verificer at task blev slettet fra databasen
            mockDbManager.Verify(db => db.DeleteTaskByName("Task1"), Times.Once);
        }
    }
}
