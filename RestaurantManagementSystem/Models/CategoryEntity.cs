using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models {
    [Table("Category")]
    public class CategoryEntity:BaseEntity {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual IList<ProductEntity> Products { get; set; }
    }
}
