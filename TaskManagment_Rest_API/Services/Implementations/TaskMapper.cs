namespace TaskManagment_Rest_API.Services.Implementations
{
    using TaskManagment_Rest_API.DTOs;
    using TaskManagment_Rest_API.Models;
    using TaskManagment_Rest_API.Services.Interfaces;
    

    public class TaskMapper : IMapper<TaskItem, TaskResponseDto>
    {
        public TaskResponseDto Map(TaskItem source)
        {
            return new TaskResponseDto
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsCompleted = source.IsCompleted,
                CreatedAt = source.CreatedAt,
                Priority = source.Priority.ToString()
            };
        }
        public IEnumerable<TaskResponseDto> Map(IEnumerable<TaskItem> sources)
        {
            return sources.Select(Map).ToList();
        }

    }
}
