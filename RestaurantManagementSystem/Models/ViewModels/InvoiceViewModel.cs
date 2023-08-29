namespace RestaurantManagementSystem.Models.ViewModels {
    public class InvoiceViewModel {
        public string? Id { get; set; }
        public string No { get; set; }
        public string PaymentWith { get; set; }
        public string? OrderNo { get; set; }
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string EmployeeId { get; set; }
        public string? TableNo { get; set; }
    }
}
