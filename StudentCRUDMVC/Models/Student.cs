using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCRUDMVC.Models {
    [Table("Student")]
    public class Student {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
