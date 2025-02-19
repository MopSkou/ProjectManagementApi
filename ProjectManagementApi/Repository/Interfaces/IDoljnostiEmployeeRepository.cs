using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface IDoljnostiEmployeeRepository
    {
        ICollection<DoljnostiEmployee> GetAll();
        DoljnostiEmployee GetById(int employeeId, int postId);
        bool Exists(int employeeId, int postId);
        bool Create(DoljnostiEmployee entity);
        bool Update(DoljnostiEmployee entity);
        bool Delete(DoljnostiEmployee entity);
        bool Save();
    }
}
