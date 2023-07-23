namespace RestaurantManagementSystem.Models.ViewModels {
    public class ProductViewModel {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public string IsTodaySpecial { get; set; }
        public string IsAvailable { get; set; }
        public int SrNo { get; set; } = 0;
    }
}
