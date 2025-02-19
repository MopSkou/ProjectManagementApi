using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class ProjectEmployees
    {
        [Key]
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public Employees Employees { get; set; }
        public Projects Projects { get; set; }
    }
}
