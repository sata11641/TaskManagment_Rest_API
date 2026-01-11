namespace TaskManagment_Rest_API.Services.Interfaces
{
    using TaskManagment_Rest_API.DTOs;

    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
        Task<TaskResponseDto?> GetTaskByIdAsync(int id);
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<TaskResponseDto>> GetCompletedTasksAsync();
        Task<IEnumerable<TaskResponseDto>> GetPendingTasksAsync();
        Task<bool> CompleteTaskAsync(int id);
    }
}