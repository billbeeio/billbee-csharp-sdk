namespace Billbee.Api.Client.Model
{
    public class Stock
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}