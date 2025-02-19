using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{

    namespace ProjectManagementApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class TasksController : Controller
        {
            private readonly ITasksRepository _tasksRepository;
            private readonly IMapper _mapper;

            public TasksController(ITasksRepository tasksRepository, IMapper mapper)
            {
                _tasksRepository = tasksRepository;
                _mapper = mapper;
            }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
            public IActionResult GetTasks()
            {
                var tasks = _mapper.Map<List<TaskDto>>(_tasksRepository.GetTasks());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(tasks);
            }

            [HttpGet("{taskId}")]
            [ProducesResponseType(200, Type = typeof(TaskDto))]
            [ProducesResponseType(400)]
            public IActionResult GetTaskById(int taskId)
            {
                if (!_tasksRepository.TaskExists(taskId))
                    return NotFound();

                var task = _mapper.Map<TaskDto>(_tasksRepository.GetTaskById(taskId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(task);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateTask([FromBody] TaskDto taskCreate)
            {
                if (taskCreate == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var taskMap = _mapper.Map<Tasks>(taskCreate);

                if (!_tasksRepository.CreateTask(taskMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }

            [HttpPut("{taskId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult UpdateTask(int taskId, [FromBody] TaskDto taskUpdate)
            {
                if (taskUpdate == null)
                    return BadRequest(ModelState);

                if (taskId != taskUpdate.TaskID)
                    return BadRequest(ModelState);

                if (!_tasksRepository.TaskExists(taskId))
                    return NotFound(new { message = "Error: Task not found" });

                if (!ModelState.IsValid)
                    return BadRequest();

                var taskMap = _mapper.Map<Tasks>(taskUpdate);

                if (!_tasksRepository.UpdateTask(taskMap))
                {
                    ModelState.AddModelError("", "Something went wrong updating task");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }

            [HttpDelete("{taskId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult DeleteTask(int taskId)
            {
                if (!_tasksRepository.TaskExists(taskId))
                {
                    return BadRequest(new { message = "Error: Invalid Id" });
                }

                var taskToDelete = _tasksRepository.GetTaskById(taskId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_tasksRepository.DeleteTask(taskToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting task");
                }

                return NoContent();
            }
        }
    }
}

