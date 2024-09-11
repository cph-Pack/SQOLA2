using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly TaskManager _taskManager;

        public TaskManagerController()
        {
            _taskManager = new TaskManager();
        }

        // GET api/task
        [HttpGet]
        public ActionResult<List<TaskClass>> GetAllTasks()
        {
            var tasks = _taskManager.GetAllTasks();
            return Ok(tasks);
        }

        // GET api/task/{name}
        [HttpGet("{name}")]
        public ActionResult<TaskClass> GetTask(string name)
        {
            var task = _taskManager.GetTask(name);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST api/task
        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskClass task)
        {
            try
            {
                _taskManager.CreateTask(task.TaskName, task.TaskValue, DateOnly.Parse(task.Deadline.ToString()), task.IsCompleted, task.Category);
                return CreatedAtAction(nameof(GetTask), new { name = task.TaskName }, task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/task/{name}
        [HttpPut("{name}")]
        public ActionResult UpdateTask(string name, [FromBody] TaskClass updatedTask)
        {
            var tasks = _taskManager.UpdateTask(name, updatedTask.TaskValue, updatedTask.Deadline, updatedTask.IsCompleted, updatedTask.Category);
            if (tasks == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/task/{name}
        [HttpDelete("{name}")]
        public ActionResult DeleteTask(string name)
        {
            _taskManager.DeleteTask(name);
            return NoContent();
        }
    }
}
