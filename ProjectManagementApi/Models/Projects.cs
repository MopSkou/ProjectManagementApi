using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int CustomerCompanyID { get; set; }
        public int ContractorCompanyID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int ProjectManagerID { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<ProjectEmployees> ProjectEmployees { get; set; }
        public Employees Employees { get; set; }
        public Companies Companies { get; set; }
    }
}
