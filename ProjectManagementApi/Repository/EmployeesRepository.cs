using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly DataContext _context;

        public EmployeesRepository(DataContext context)
        {
            _context = context;
        }

        public bool EmployeeExists(int employeeId)
        {
            return _context.Employees.Any(e => e.EmployeeID == employeeId);
        }

        public Employees GetEmployeeById(int employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
        }

        public ICollection<Employees> GetEmployees()
        {
            return _context.Employees.OrderBy(e => e.EmployeeID).ToList();
        }

        public bool CreateEmployee(Employees employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool UpdateEmployee(Employees employee)
        {
            _context.Update(employee);
            return Save();
        }

        public bool DeleteEmployee(Employees employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}