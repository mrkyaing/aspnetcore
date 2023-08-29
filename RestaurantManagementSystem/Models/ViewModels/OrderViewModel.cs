namespace RestaurantManagementSystem.Models.ViewModels {
    public class OrderViewModel {
        public string? Id { get; set; }
        public string No { get; set; }
        public string IsParcel { get; set; }
        public DateTime OrderedAt { get; set; } 
        public string? Status { get; set; }
        public string EmployeeId { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
        public string EmployeeNo { get; set; }
        public string TableId { get; set; }
        public string TableNo { get; set; }
        public virtual TableEntity Table { get; set; }
        public virtual  OrderDetailViewModel[] orderDetails { get;set; }
    }
}
