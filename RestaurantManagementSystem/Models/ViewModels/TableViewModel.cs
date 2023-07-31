namespace RestaurantManagementSystem.Models.ViewModels {
    public class TableViewModel {
        public string Id { get; set; }
        public string No { get; set; }
        public string IsAvailable { get; set; }
        public int AvailableCapacityPerson { get; set; }
        public string Status { get; set; }
    }
}
