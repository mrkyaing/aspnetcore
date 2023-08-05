using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("OrderDetail")]
    public class OrderDetailEntity:BaseEntity {
        public string OrderId { get; set; }//Foreign Key ofr OrderId
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
    }
}
