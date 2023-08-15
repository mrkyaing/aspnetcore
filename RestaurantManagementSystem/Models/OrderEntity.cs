using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Order")]
    public class OrderEntity:BaseEntity {
        public string No { get; set; }
        public bool IsParcel { get; set; }
        public string? Status { get; set; }
        [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; }
        public virtual EmployeeEntity? Employee { get; set; }
        [ForeignKey("TableId")]
        public string TableId { get; set; }
        public virtual TableEntity? Table { get; set; }
    }
}
