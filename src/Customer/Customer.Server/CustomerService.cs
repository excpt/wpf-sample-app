namespace Sample.App.Customer.Server
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Data;
    using Data.Service;

    using Google.Protobuf.WellKnownTypes;

    using Grpc.Core;

    using Microsoft.EntityFrameworkCore;

    using Customer = Data.Models.Customer;

    public class CustomerService
        : Data.Service.CustomerService.CustomerServiceBase
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerService(CustomerDbContext dbContext) =>
            _dbContext = dbContext;

        public override async Task<GetResponse> Get(
            Request request,
            ServerCallContext context
        )
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(
                c => c.Id == request.Id
            );

            var response = new GetResponse
            {
                Customer = MapCustomerToGrpc(
                    customer ?? new Customer()
                )
            };

            return await Task.FromResult(
                response
            );
        }

        public override async Task<ListResponse> List(
            CustomerRequest request,
            ServerCallContext context
        )
        {
            var filter = request.Customer;
            var customerQuery = _dbContext.Customers.AsQueryable();
            var response = new ListResponse();

            if (!string.IsNullOrEmpty(
                filter.CompanyName
            ))
            {
                customerQuery = customerQuery.Where(
                    c => EF.Functions.Like(
                        c.CompanyName,
                        $"%{filter.CompanyName}%"
                    )
                );
            }

            if (!string.IsNullOrEmpty(
                filter.FirstName
            ))
            {
                customerQuery = customerQuery.Where(
                    c => EF.Functions.Like(
                        c.CompanyName,
                        $"%{filter.FirstName}%"
                    )
                );
            }

            if (!string.IsNullOrEmpty(
                filter.LastName
            ))
            {
                customerQuery = customerQuery.Where(
                    c => EF.Functions.Like(
                        c.CompanyName,
                        $"%{filter.LastName}%"
                    )
                );
            }

            foreach (var customer in customerQuery)
            {
                response.Customer.Add(
                    MapCustomerToGrpc(
                        customer
                    )
                );
            }

            return await Task.FromResult(
                response
            );
        }

        public override async Task<Response> Activate(
            Request request,
            ServerCallContext context
        )
        {
            var response = new Response();

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(
                c => c.Id == request.Id
            );

            if (customer is null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;

                response.StatusMessage =
                    $"Customer with Id = {request.Id} not found";

                response.Success = false;

                return await Task.FromResult(
                    response
                );
            }

            customer.Active = true;

            await _dbContext.SaveChangesAsync();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusMessage = "Customer activated";
            response.Success = true;

            return await Task.FromResult(
                response
            );
        }

        public override async Task<Response> Deactivate(
            Request request,
            ServerCallContext context
        )
        {
            var response = new Response();

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(
                c => c.Id == request.Id
            );

            if (customer is null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;

                response.StatusMessage =
                    $"Customer with Id = {request.Id} not found";

                response.Success = false;

                return await Task.FromResult(
                    response
                );
            }

            customer.Active = false;

            await _dbContext.SaveChangesAsync();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusMessage = "Customer deactivated";
            response.Success = true;

            return await Task.FromResult(
                response
            );
        }

        public override async Task<Response> Update(
            CustomerRequest request,
            ServerCallContext context
        )
        {
            var response = new Response();

            var customer = MapCustomerFromGrpc(
                request.Customer
            );

            var customerToUpdate =
                await _dbContext.Customers.FirstOrDefaultAsync(
                    c => c.Id == customer.Id
                );

            if (customerToUpdate is null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;

                response.StatusMessage =
                    $"Customer with Id = {customer.Id} not found";

                response.Success = false;

                return await Task.FromResult(
                    response
                );
            }

            customerToUpdate.Active = customer.Active;
            customerToUpdate.CompanyName = customer.CompanyName;
            customerToUpdate.FirstName = customer.FirstName;
            customerToUpdate.LastName = customer.LastName;
            customerToUpdate.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusMessage = "Customer updated";
            response.Success = true;

            return await Task.FromResult(
                response
            );
        }

        public override async Task<Response> Create(
            CustomerRequest request,
            ServerCallContext context
        )
        {
            var customer = MapCustomerFromGrpc(
                request.Customer
            );

            await _dbContext.Customers.AddAsync(
                customer
            );

            await _dbContext.SaveChangesAsync();

            var response = new Response
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.Accepted,
                StatusMessage = "Customer created"
            };

            return await Task.FromResult(
                response
            );
        }

        private static Customer MapCustomerFromGrpc(
            Data.Service.Customer grpcCustomer
        )
        {
            var customer = new Customer
            {
                Active = grpcCustomer.Active,
                CompanyName = grpcCustomer.CompanyName,
                CreatedAt = grpcCustomer.CreatedAt.ToDateTime(),
                FirstName = grpcCustomer.FirstName,
                LastName = grpcCustomer.LastName,
                UpdatedAt = grpcCustomer.UpdatedAt.ToDateTime()
            };

            return customer;
        }

        private static Data.Service.Customer MapCustomerToGrpc(
            Customer customer
        )
        {
            var grpcCustomer = new Data.Service.Customer
            {
                Id = customer.Id,
                Active = customer.Active,
                CompanyName = customer.CompanyName,
                CreatedAt = Timestamp.FromDateTime(
                    customer.CreatedAt
                ),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UpdatedAt = Timestamp.FromDateTime(
                    customer.UpdatedAt
                )
            };

            return grpcCustomer;
        }
    }
}
