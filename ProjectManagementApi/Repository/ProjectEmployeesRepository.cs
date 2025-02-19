using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class ProjectEmployeesRepository : IProjectEmployeesRepository
    {
        private readonly DataContext _context;

        public ProjectEmployeesRepository(DataContext context)
        {
            _context = context;
        }

        public bool Exists(int projectId, int employeeId)
        {
            return _context.ProjectEmployees.Any(pe => pe.ProjectID == projectId && pe.EmployeeID == employeeId);
        }

        public ProjectEmployees GetById(int projectId, int employeeId)
        {
            return _context.ProjectEmployees.FirstOrDefault(pe => pe.ProjectID == projectId && pe.EmployeeID == employeeId);
        }

        public ICollection<ProjectEmployees> GetAll()
        {
            return _context.ProjectEmployees.ToList();
        }

        public bool Create(ProjectEmployees entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Update(ProjectEmployees entity)
        {
            _context.Update(entity);
            return Save();
        }

        public bool Delete(ProjectEmployees entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
