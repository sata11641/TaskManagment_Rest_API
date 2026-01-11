namespace TaskManagment_Rest_API.Repositories.Implementations
{
    using TaskManagment_Rest_API.Models;
    using TaskManagment_Rest_API.Repositories.Interfaces;

    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks;
        private int _nextId = 1;

        public TaskRepository()
        {
            _tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = _nextId++,
                    Title = "Learn SOLID principles",
                    Description = "Study and understand all 5 SOLID principles",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    Priority = Priority.High
                },
                new TaskItem
                {
                    Id = _nextId++,
                    Title = "Build REST API",
                    Description = "Create a REST API using .NET 8",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    Priority = Priority.Medium
                }
            };
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await Task.FromResult(_tasks);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            return await Task.FromResult(task);
        }

        public async Task<TaskItem> CreateAsync(TaskItem entity)
        {
            entity.Id = _nextId++;
            entity.CreatedAt = DateTime.UtcNow;
            _tasks.Add(entity);
            return await Task.FromResult(entity);
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem entity)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
                return await Task.FromResult<TaskItem?>(null);

            existingTask.Title = entity.Title;
            existingTask.Description = entity.Description;
            existingTask.IsCompleted = entity.IsCompleted;
            existingTask.Priority = entity.Priority;

            if (entity.IsCompleted && !existingTask.IsCompleted)
                existingTask.CompletedAt = DateTime.UtcNow;

            return await Task.FromResult(existingTask);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return await Task.FromResult(false);

            _tasks.Remove(task);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<TaskItem>> GetByCompletionStatusAsync(bool isCompleted)
        {
            var tasks = _tasks.Where(t => t.IsCompleted == isCompleted);
            return await Task.FromResult(tasks);
        }

        public async Task<IEnumerable<TaskItem>> GetByPriorityAsync(Priority priority)
        {
            var tasks = _tasks.Where(t => t.Priority == priority);
            return await Task.FromResult(tasks);
        }

        public async Task<bool> MarkAsCompletedAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return await Task.FromResult(false);

            task.IsCompleted = true;
            task.CompletedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }
    }
}