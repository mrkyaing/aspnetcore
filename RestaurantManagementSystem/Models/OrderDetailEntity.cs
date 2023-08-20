using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("OrderDetail")]
    public class OrderDetailEntity:BaseEntity {
        [ForeignKey("OrderId")]
        public string OrderId { get; set; }//Foreign Key for OrderId
        public virtual OrderEntity Order { get; set; }
        [ForeignKey("ProductId")]
        public string ProductId { get; set; }
        public virtual ProductEntity Product{ get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
    }
}
