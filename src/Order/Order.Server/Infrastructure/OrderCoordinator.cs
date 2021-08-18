namespace Sample.App.Order.Server.Infrastructure
{
    using Akka.Actor;

    using Messages;

    public class OrderCoordinator : ReceiveActor
    {
        public OrderCoordinator()
        {
            Receive<CreateOrder>(CreateOrderImpl);
        }

        private bool CreateOrderImpl(CreateOrder message)
        {
            if (!Context.Child(
                    $"orderId-{message.Id}"
                )
                .IsNobody())
            {
                return false;
            }

            var actorRef = Context.ActorOf(
                Props.Create<OrderActor>(),
                $"orderId-{message.Id}"
            );

            actorRef.Forward(message);
            
            return true;
        }
    }
}
