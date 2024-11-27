using Microsoft.AspNetCore.Mvc;
using TodoList.Core.Contracts;
using TodoList.Core.Models;

namespace TodoList.RESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        #region Fields

        private readonly ITaskItemService _service;

        #endregion

        #region Constructors

        public TaskController(ITaskItemService service)
        {
            _service = service;
        }

        #endregion

        #region Methods

        [HttpGet()]
        public IEnumerable<TaskItem> Indeks()
        {
            return _service.GetAll();
        }

        [HttpGet()]
        [Route("Toady")]
        public IEnumerable<TaskItem> GetForToday()
        {
            return _service.GetTasksForToday();
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(Guid id)
        {
            var task = _service.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem taskItem)
        {
            if (taskItem == null)
            {
                return BadRequest("TaskItem cannot be null.");
            }

            var createdTask = _service.Add(taskItem);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Guid id, [FromBody] TaskItem taskItem)
        {
            if (taskItem == null || taskItem.Id != id)
            {
                return BadRequest("TaskItem data is invalid.");
            }
            _service.Update(taskItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var task = _service.GetById(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            _service.Delete(id);
            return NoContent();
        }


        [HttpPost()]        
        [Route("{id}/items")]
        public ActionResult<TaskItem> AddSubItem(Guid id, [FromBody] TaskISubtem taskItem)
        {
            if (taskItem == null)
            {
                return BadRequest("TaskItem cannot be null.");
            }
            var task = _service.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            _service.AddSubItem(task, taskItem);
            return Ok();
        }

        [HttpDelete()]
        [Route("{id}/{subitem_id}")]
        public ActionResult<TaskItem> AddSubItem(Guid id, Guid subitem_id)
        {
            _service.RemoveSubItem(id, subitem_id);
            return Ok();
        }


        [HttpPut("done/{id}")]
        public IActionResult SetAsDoe(Guid id)
        {
            var flag = _service.SetAsDone(id);
            if (!flag)
            {
                return NotFound("Task not found.");
            }
            return NoContent();
        }

        [HttpPut("done/{id}/{subitem_id}")]
        public IActionResult SetChildAsDoe(Guid id, Guid subitem_id)
        {
            var flag = _service.SetChildAsDone(id, subitem_id);
            if (!flag)
            {
                return NotFound("Task not found.");
            }
            return NoContent();
        }

        #endregion


    }
}