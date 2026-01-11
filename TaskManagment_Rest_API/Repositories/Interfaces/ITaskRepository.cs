namespace TaskManagment_Rest_API.Repositories.Interfaces
{
    using TaskManagment_Rest_API.Models;

    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetByCompletionStatusAsync(bool isCompleted);
        Task<IEnumerable<TaskItem>> GetByPriorityAsync(Priority priority);
        Task<bool> MarkAsCompletedAsync(int id);
    }
}