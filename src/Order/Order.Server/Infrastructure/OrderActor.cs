namespace Sample.App.Order.Server.Infrastructure
{
    using Akka.Actor;

    using Data;
    using Data.Models;

    using Messages;

    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<CreateOrder>(
                CreateOrderImpl
            );
        }

        private bool CreateOrderImpl(CreateOrder message)
        {
            using var db = OrderDbContext.CreateContext();

            var order = new Order();

            db.Orders.Add(
                order
            );

            db.SaveChanges();

            return true;
        }
    }
}
