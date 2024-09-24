using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly TaskManager _taskManager;

        public TaskManagerController(TaskManager taskManager)
        {
            //_taskManager = new TaskManager();
            _taskManager = taskManager;
        }

        // GET api/task
        [HttpGet]
        public ActionResult<List<TaskClass>> GetAllTasks()
        {
            try
            {
                var tasks = _taskManager.GetAllTasks();
                return Ok(tasks);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/task/{name}
        [HttpGet("{name}")]
        public ActionResult<TaskClass> GetTask(string name)
        {
            try
            {
                var task = _taskManager.GetTask(name);
                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/task
        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskClass task)
        {
            try
            {
                _taskManager.CreateTask(task);
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
            try
            {
                var tasks = _taskManager.UpdateTask(updatedTask, name);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // DELETE api/task/{name}
        [HttpDelete("{name}")]
        public ActionResult DeleteTask(string name)
        {
            try
            {
                _taskManager.DeleteTask(name);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
