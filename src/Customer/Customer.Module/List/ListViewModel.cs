namespace Sample.App.Customer.Module.List
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Prism.Commands;
    using Prism.Mvvm;

    using Remote.CustomerService;

    public class ListViewModel : BindableBase
    {
        private readonly CustomerService.CustomerServiceClient _grpc;
        private List<GrpcCustomer> _customers;
        private bool _isEnabled;

        private bool _isLoading;

        public ListViewModel(CustomerService.CustomerServiceClient grpc)
        {
            _grpc = grpc;

            LoadData = new DelegateCommand(
                async () =>
                {
                    IsLoading = true;
                    await LoadGrpcData();
                    IsLoading = false;
                }
            );
        }

        public DelegateCommand LoadData { get; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(
                    ref _isLoading,
                    value
                );

                IsEnabled = !value;
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set =>
                SetProperty(
                    ref _isEnabled,
                    value
                );
        }

        public IEnumerable<GrpcCustomer> Customers
        {
            get => _customers;
            set =>
                SetProperty(
                    ref _customers,
                    (List<GrpcCustomer>)value
                );
        }

        private async Task LoadGrpcData()
        {
            var customers = await _grpc.ListAsync(
                new CustomerRequest()
            );

            Customers = customers.Customer.ToList();
        }
    }
}
