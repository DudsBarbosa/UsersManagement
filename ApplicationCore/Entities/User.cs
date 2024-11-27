using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        [Range(0, 9999999999.99)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal HourValue { get; set; }
        public DateTime AddDate { get; set; }
        public bool Active { get; set; }
    }
}
