﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI.Controllers;
using TaskManagerAPI;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPITests
{
    public class TaskManagerIntegrationTests
    {
        private readonly TaskManagerController _controller;
        private readonly DBManager _dbManager;
        private readonly TaskManager _taskManager;

        public TaskManagerIntegrationTests()
        {
            // Arrange - setup test database
            _dbManager = new DBManager();  // Assumes it connects to a test instance
            _taskManager = new TaskManager(_dbManager);
            _controller = new TaskManagerController(_taskManager);
        }

        [Fact]
        public async Task CreateTaskAndGetTask_ShouldReturnTheCreatedTask()
        {
            // Arrange
            var newTask = new TaskClass
            {
                TaskName = "IntegrationTestTask1",
                TaskValue = "TestValue1",
                Deadline = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Category = "TestCategory1"
            };

            //var controller = new TaskManagerController();

            // Act: Opret en ny opgave via API'et
            var createResult = _controller.CreateTask(newTask);

            // Assert: Oprettelse skal returnere status 201 (Created)
            Assert.IsType<CreatedAtActionResult>(createResult);

            // Act: Hent den oprettede opgave via API'et
            var getResult = _controller.GetTask("IntegrationTestTask1");

            // Assert: Opgaven skal returneres korrekt med de samme værdier
            var okResult = Assert.IsType<OkObjectResult>(getResult.Result);
            var task = Assert.IsType<TaskClass>(okResult.Value);
            Assert.Equal("IntegrationTestTask1", task.TaskName);
            Assert.Equal("TestValue1", task.TaskValue);
        }

        [Fact]
        public async Task UpdateTask_ShouldModifyTheExistingTask()
        {
            // Arrange: Opret en opgave, der skal opdateres
            var task = new TaskClass
            {
                TaskName = "IntegrationTestTask2",
                TaskValue = "OriginalValue",
                Deadline = DateTime.Now.AddDays(2),
                IsCompleted = false,
                Category = "OriginalCategory"
            };

            //var controller = new TaskManagerController();
            _controller.CreateTask(task);

            // Act: Opdater opgaven med nye værdier
            var updatedTask = new TaskClass
            {
                TaskName = "IntegrationTestTask2",  // Samme navn, da vi opdaterer denne opgave
                TaskValue = "UpdatedValue",
                Deadline = DateTime.Now.AddDays(3),
                IsCompleted = true,
                Category = "UpdatedCategory"
            };
            var updateResult = _controller.UpdateTask("IntegrationTestTask2", updatedTask);

            // Assert: Opdatering skal returnere status 204 (NoContent)
            Assert.IsType<NoContentResult>(updateResult);

            // Act: Hent den opdaterede opgave
            var getResult = _controller.GetTask("IntegrationTestTask2");

            // Assert: Bekræft at opgaven er opdateret korrekt
            var okResult = Assert.IsType<OkObjectResult>(getResult.Result);
            var taskFromDb = Assert.IsType<TaskClass>(okResult.Value);
            Assert.Equal("UpdatedValue", taskFromDb.TaskValue);
            Assert.True(taskFromDb.IsCompleted);
            Assert.Equal("UpdatedCategory", taskFromDb.Category);
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTaskFromDatabase()
        {
            //var controller = new TaskManagerController();

            // Vi sletter opgaverne
            var deleteResult = _controller.DeleteTask("IntegrationTestTask1");
            var deleteResuult = _controller.DeleteTask("IntegrationTestTask2");

            Assert.IsType<NoContentResult>(deleteResult);
            Assert.IsType<NoContentResult>(deleteResuult);

            // Act: Forsøg at hente den slettede opgave
            var getResult1 = _controller.GetTask("IntegrationTestTask1");
            var getResult2 = _controller.GetTask("IntegrationTestTask2");

            // Assert: Opgaverne skal ikke længere kunne findes
            Assert.IsType<NotFoundObjectResult>(getResult1.Result);
            Assert.IsType<NotFoundObjectResult>(getResult2.Result);
        }

    }
}
