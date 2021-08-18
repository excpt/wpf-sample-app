namespace Order.Cmd
{
    using System;
    using System.Threading.Tasks;

    using Grpc.Net.Client;

    using Remote.OrderService;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress(
                "https://localhost:5001"
            );

            var client = new OrderService.OrderServiceClient(
                channel
            );

            for (var i = 0; i < 10_000; i++)
            {
                var response = await client.CreateAsync(
                    new CreateOrder
                    {
                        Id = Guid
                            .NewGuid()
                            .ToString()
                    }
                );

                if (!response.Success)
                {
                    Console.WriteLine(
                        "Error:"
                    );

                    Console.WriteLine(
                        response.StatusCode
                    );

                    Console.WriteLine(
                        response.StatusMessage
                    );
                }
            }

            var list = await client.ListAsync(
                new OrderRequest()
            );

            foreach (var order in list.Orders)
            {
                Console.WriteLine(
                    order.Id
                );
            }
        }
    }
}
