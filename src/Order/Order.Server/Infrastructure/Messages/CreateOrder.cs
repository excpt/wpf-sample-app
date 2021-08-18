namespace Sample.App.Order.Server.Infrastructure.Messages
{
    using Remote.OrderService;

    public class CreateOrder
    {
        public CreateOrder(string id, Order order)
        {
            Id = id;
            Order = order;
        }

        public string Id { get; }
        public Order Order { get; }
    }
}
