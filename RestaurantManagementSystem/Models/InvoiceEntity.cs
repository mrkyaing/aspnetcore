using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Invoice")]
    public class InvoiceEntity : BaseEntity {
        public string No { get; set; }
        public string PaymentWith { get; set; }
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string EmployeeId { get; set; }
    }
}
