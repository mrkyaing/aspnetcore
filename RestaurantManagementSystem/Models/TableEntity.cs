using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Tables")]
    public class TableEntity:BaseEntity {
        public string No { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableCapacityPerson { get; set; }
    }
}
