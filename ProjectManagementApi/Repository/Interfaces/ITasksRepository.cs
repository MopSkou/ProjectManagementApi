using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface ITasksRepository
    {
        ICollection<Tasks> GetTasks();
        Tasks GetTaskById(int taskId);
        bool TaskExists(int taskId);
        bool CreateTask(Tasks task);
        bool UpdateTask(Tasks task);
        bool DeleteTask(Tasks task);
        bool Save();
    }
}