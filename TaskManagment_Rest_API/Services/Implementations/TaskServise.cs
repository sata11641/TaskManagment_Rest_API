namespace TaskManagment_Rest_API.Services.Implementations
{
    using TaskManagment_Rest_API.DTOs;
    using TaskManagment_Rest_API.Models;
    using TaskManagment_Rest_API.Repositories.Interfaces;
    using TaskManagment_Rest_API.Services.Interfaces;

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper<TaskItem, TaskResponseDto> _mapper;

        // Dependency Injection через конструктор
        public TaskService(
            ITaskRepository repository,
            IMapper<TaskItem, TaskResponseDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return _mapper.Map(tasks);
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task != null ? _mapper.Map(task) : null;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = (Priority)taskDto.Priority,
                IsCompleted = false
            };

            var createdTask = await _repository.CreateAsync(task);
            return _mapper.Map(createdTask);
        }

        public async Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
                return null;

            // Обновляем только переданные поля
            if (taskDto.Title != null)
                existingTask.Title = taskDto.Title;

            if (taskDto.Description != null)
                existingTask.Description = taskDto.Description;

            if (taskDto.IsCompleted.HasValue)
            {
                existingTask.IsCompleted = taskDto.IsCompleted.Value;
                if (taskDto.IsCompleted.Value)
                    existingTask.CompletedAt = DateTime.UtcNow;
            }

            if (taskDto.Priority.HasValue)
                existingTask.Priority = (Priority)taskDto.Priority.Value;

            var updatedTask = await _repository.UpdateAsync(id, existingTask);
            return updatedTask != null ? _mapper.Map(updatedTask) : null;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskResponseDto>> GetCompletedTasksAsync()
        {
            var tasks = await _repository.GetByCompletionStatusAsync(true);
            return _mapper.Map(tasks);
        }

        public async Task<IEnumerable<TaskResponseDto>> GetPendingTasksAsync()
        {
            var tasks = await _repository.GetByCompletionStatusAsync(false);
            return _mapper.Map(tasks);
        }

        public async Task<bool> CompleteTaskAsync(int id)
        {
            return await _repository.MarkAsCompletedAsync(id);
        }
    }
}
