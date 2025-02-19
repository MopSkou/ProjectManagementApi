using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApi.Models
{
    public class Doljnosti
    {
        [Key]
        public int id_doljnosti { get; set; }
        public string Post { get; set; }
        public string Password { get; set; }
        public ICollection<DoljnostiEmployee> DoljnostiEmployee { get; set; }
    }
}
