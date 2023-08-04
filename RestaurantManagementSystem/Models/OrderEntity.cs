using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Order")]
    public class OrderEntity:BaseEntity {
        public string No { get; set; }
        public bool IsParcel { get; set; }
        public string? Status { get; set; }
        public string EmployeeId { get; set; }
        public string TableId { get; set; }
    }
}
