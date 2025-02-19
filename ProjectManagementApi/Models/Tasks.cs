using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int AuthorID { get; set; }
        public int PerformerID { get; set; }
        public int ProjectID { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
        public Employees Employees { get; set; }
        public Projects Projects { get; set; }
    }
}
