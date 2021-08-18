namespace Sample.App.Customer.Server
{
    using System.Threading.Tasks;

    using Data;
    using Data.Service;

    using Grpc.Core;

    using Customer = Data.Models.Customer;

    public class CustomerService
        : Data.Service.CustomerService.CustomerServiceBase
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerService(CustomerDbContext dbContext) =>
            _dbContext = dbContext;

        public override async Task<CreateResponse> Create(
            CreateRequest request,
            ServerCallContext context
        )
        {
            var grpcCustomer = request.Customer;

            var customer = new Customer
            {
                Active = grpcCustomer.Active,
                CompanyName = grpcCustomer.CompanyName,
                CreatedAt = grpcCustomer.CreatedAt.ToDateTime(),
                FirstName = grpcCustomer.FirstName,
                LastName = grpcCustomer.LastName,
                UpdatedAt = grpcCustomer.UpdatedAt.ToDateTime()
            };

            await _dbContext.Customers.AddAsync(
                customer
            );

            await _dbContext.SaveChangesAsync();

            var response = new CreateResponse();

            return await Task.FromResult(
                response
            );
        }
    }
}
