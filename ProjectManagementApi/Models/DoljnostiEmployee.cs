using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class DoljnostiEmployee
    {
        [Key]
        public int EmployeeID { get; set; }
        public int PostID { get; set; }
        public Doljnosti Doljnosti { get; set; }
        public Employees Employees { get; set; }
    }
}
