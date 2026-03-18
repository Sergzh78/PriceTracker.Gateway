namespace PriceTracker.Gateway.Entities
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;      
        public string Domain { get; set; } = string.Empty;    
        public bool IsActive { get; set; } = true;                    
    }
}
