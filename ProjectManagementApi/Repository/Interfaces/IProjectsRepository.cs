using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface IProjectsRepository
    {
        ICollection<Projects> GetProjects();
        Projects GetProjectById(int projectId);
        bool ProjectExists(int projectId);
        bool CreateProject(Projects project);
        bool UpdateProject(Projects project);
        bool DeleteProject(Projects project);
        bool Save();
    }
}