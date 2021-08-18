namespace Sample.App.Order.Server
{
    using Akka.Actor;

    using Infrastructure;

    public class AkkaOrderService
    {
        public AkkaOrderService(ActorSystem actorSystem = null)
        {
            System = actorSystem
                     ?? ActorSystem.Create(
                         nameof(AkkaOrderService)
                     );

            Orders = System.ActorOf(
                Props.Create<OrderCoordinator>(),
                nameof(OrderCoordinator)
            );
        }

        public ActorSystem System { get; }

        public IActorRef Orders { get; }
    }
}
