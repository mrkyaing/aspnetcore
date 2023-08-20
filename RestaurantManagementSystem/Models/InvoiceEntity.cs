using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Invoice")]
    public class InvoiceEntity : BaseEntity {
        public string No { get; set; }
        public string PaymentWith { get; set; }
       // [ForeignKey("OrderId")]
        public string OrderId { get; set; }
       // public virtual OrderEntity Order { get; set; }
        public decimal TotalAmount { get; set; }
       // [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; }
      //  public  virtual   EmployeeEntity    Employee { get; set; }
    }
}
