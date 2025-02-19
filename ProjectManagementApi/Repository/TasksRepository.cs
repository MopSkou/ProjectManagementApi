using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly DataContext _context;

        public TasksRepository(DataContext context)
        {
            _context = context;
        }

        public bool TaskExists(int taskId)
        {
            return _context.Tasks.Any(t => t.TaskID == taskId);
        }

        public Tasks GetTaskById(int taskId)
        {
            return _context.Tasks.FirstOrDefault(t => t.TaskID == taskId);
        }

        public ICollection<Tasks> GetTasks()
        {
            return _context.Tasks.OrderBy(t => t.TaskID).ToList();
        }

        public bool CreateTask(Tasks task)
        {
            _context.Add(task);
            return Save();
        }

        public bool UpdateTask(Tasks task)
        {
            _context.Update(task);
            return Save();
        }

        public bool DeleteTask(Tasks task)
        {
            _context.Remove(task);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}