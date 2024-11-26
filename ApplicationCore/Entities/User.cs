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
