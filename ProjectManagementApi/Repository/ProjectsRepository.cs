using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly DataContext _context;

        public ProjectsRepository(DataContext context)
        {
            _context = context;
        }

        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Any(p => p.ProjectID == projectId);
        }

        public Projects GetProjectById(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.ProjectID == projectId);
        }

        public ICollection<Projects> GetProjects()
        {
            return _context.Projects.OrderBy(p => p.ProjectID).ToList();
        }

        public bool CreateProject(Projects project)
        {
            _context.Add(project);
            return Save();
        }

        public bool UpdateProject(Projects project)
        {
            _context.Update(project);
            return Save();
        }

        public bool DeleteProject(Projects project)
        {
            _context.Remove(project);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
