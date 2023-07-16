using System.ComponentModel.DataAnnotations;
namespace SchoolInfoMVC.Models {
    public class BaseEntity {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public DateTime UpdatedAt { get; set;}
        public string Ip { get; set; }
    }
}
