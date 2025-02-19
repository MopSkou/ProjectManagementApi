using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProjectManagementApi.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public ICollection<ProjectEmployees> ProjectEmployees { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<Projects> Projects { get; set; }
        public ICollection<DoljnostiEmployee> DoljnostiEmployees { get; set; }
    }
}
