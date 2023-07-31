using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("OrderDetail")]
    public class OrderDetailEntity:BaseEntity {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
    }
}
