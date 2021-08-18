namespace Sample.App.Order.Server
{
    using System.Net;
    using System.Threading.Tasks;

    using Akka.Actor;

    using Data;

    using Grpc.Core;

    using Remote.OrderService;

    public class WebOrderService : OrderService.OrderServiceBase
    {
        private readonly OrderDbContext _orderContext;
        private readonly AkkaOrderService _orderService;

        public WebOrderService(
            AkkaOrderService orderService,
            OrderDbContext orderContext
        )
        {
            _orderService = orderService;
            _orderContext = orderContext;
        }

        public override Task<Response> Create(
            CreateOrder request,
            ServerCallContext context
        )
        {
            var response = new Response();

            var message = new Infrastructure.Messages.CreateOrder(
                request.Id,
                request.Order
            );

            _orderService.Orders.Tell(
                message
            );

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusMessage = "Order placed";
            response.Success = true;

            return Task.FromResult(
                response
            );
        }

        public override Task<ListResponse> List(
            OrderRequest request,
            ServerCallContext context
        )
        {
            var response = new ListResponse();

            foreach (var order in _orderContext.Orders)
            {
                response.Orders.Add(
                    new Order
                    {
                        Id = order.Id
                    }
                );
            }

            return Task.FromResult(
                response
            );
        }
    }
}
