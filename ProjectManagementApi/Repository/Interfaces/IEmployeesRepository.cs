using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface IEmployeesRepository
    {
        ICollection<Employees> GetEmployees();
        Employees GetEmployeeById(int employeeId);
        bool EmployeeExists(int employeeId);
        bool CreateEmployee(Employees employee);
        bool UpdateEmployee(Employees employee);
        bool DeleteEmployee(Employees employee);
        bool Save();
    }
}