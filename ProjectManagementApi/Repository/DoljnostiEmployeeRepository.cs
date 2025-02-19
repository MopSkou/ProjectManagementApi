using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class DoljnostiEmployeeRepository : IDoljnostiEmployeeRepository
    {
        private readonly DataContext _context;

        public DoljnostiEmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public bool Exists(int employeeId, int postId)
        {
            return _context.DoljnostiEmployees.Any(de => de.EmployeeID == employeeId && de.PostID == postId);
        }

        public DoljnostiEmployee GetById(int employeeId, int postId)
        {
            return _context.DoljnostiEmployees.FirstOrDefault(de => de.EmployeeID == employeeId && de.PostID == postId);
        }

        public ICollection<DoljnostiEmployee> GetAll()
        {
            return _context.DoljnostiEmployees.ToList();
        }

        public bool Create(DoljnostiEmployee entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(DoljnostiEmployee entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public bool Update(DoljnostiEmployee entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}