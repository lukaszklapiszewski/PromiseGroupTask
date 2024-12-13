namespace ApiClient.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    }
}
