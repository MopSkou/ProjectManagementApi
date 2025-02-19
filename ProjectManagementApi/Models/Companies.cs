using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class Companies
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
