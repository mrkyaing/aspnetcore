namespace RestaurantManagementSystem.Models.ViewModels {
    public class TableViewModel {
        public string Id { get; set; }
        public string No { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableCapacityPerson { get; set; }
    }
}
