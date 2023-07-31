namespace RestaurantManagementSystem.Models.ViewModels {
    public class OrderDetailViewModel {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
        public virtual List<ProductViewModel>? Products { get; set; }
    }
}
