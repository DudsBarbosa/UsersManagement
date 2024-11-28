using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class User : BaseEntity
    {        
        public required string Name { get; set; }
        public decimal HourValue { get; set; }
        public DateTime AddDate { get; set; }
        public bool Active { get; set; }
    }
}
