namespace RestaurantManagementSystem.Models.ViewModels {
    public class EmployeeViewModel {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? MobilePhone { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? Position { get; set; }
        public string Address { get; set; }
        public string NRC { get; set; }
        public decimal Salary { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
