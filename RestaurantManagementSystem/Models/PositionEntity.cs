using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Position")]
    public class PositionEntity:BaseEntity {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
}
