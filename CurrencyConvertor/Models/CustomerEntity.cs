using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConvertor.Models
{
    [Table("Customer")]
    public class CustomerEntity
    {
        [Column("Id")]
        [Key]
        public string Id { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
