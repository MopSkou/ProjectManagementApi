using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface IProjectEmployeesRepository
    {
        ICollection<ProjectEmployees> GetAll();
        ProjectEmployees GetById(int projectId, int employeeId);
        bool Exists(int projectId, int employeeId);
        bool Create(ProjectEmployees entity);
        bool Update(ProjectEmployees entity);
        bool Delete(ProjectEmployees entity);
        bool Save();
    }
}