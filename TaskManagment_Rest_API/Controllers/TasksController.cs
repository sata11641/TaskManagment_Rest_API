// Controllers/TasksController.cs
// Single Responsibility - отвечает только за обработку HTTP запросов
using Microsoft.AspNetCore.Mvc;
using TaskManagment_Rest_API.DTOs;
using TaskManagment_Rest_API.Services.Interfaces;

namespace TaskManagment_Rest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Получить все задачи
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Получить задачу по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound(new { message = $"Task with ID {id} not found" });

            return Ok(task);
        }

        /// <summary>
        /// Получить завершенные задачи
        /// </summary>
        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetCompletedTasks()
        {
            var tasks = await _taskService.GetCompletedTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Получить незавершенные задачи
        /// </summary>
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetPendingTasks()
        {
            var tasks = await _taskService.GetPendingTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Создать новую задачу
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTask = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(
                nameof(GetTask),
                new { id = createdTask.Id },
                createdTask);
        }

        /// <summary>
        /// Обновить задачу
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(
            int id,
            [FromBody] UpdateTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);

            if (updatedTask == null)
                return NotFound(new { message = $"Task with ID {id} not found" });

            return Ok(updatedTask);
        }

        /// <summary>
        /// Отметить задачу как выполненную
        /// </summary>
        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> CompleteTask(int id)
        {
            var result = await _taskService.CompleteTaskAsync(id);

            if (!result)
                return NotFound(new { message = $"Task with ID {id} not found" });

            return NoContent();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if (!result)
                return NotFound(new { message = $"Task with ID {id} not found" });

            return NoContent();
        }
    }
}