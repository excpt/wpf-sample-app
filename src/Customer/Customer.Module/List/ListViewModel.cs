namespace Sample.App.Customer.Module.List
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using Card;

    using Kernel.Desktop;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Regions;

    using Remote.CustomerService;

    [SuppressMessage(
        "ReSharper",
        "AsyncVoidLambda"
    )]
    public class ListViewModel : BindableBase
    {
        private readonly CustomerService.CustomerServiceClient _grpc;
        private readonly IRegionManager _regionManager;
        private List<GrpcCustomer> _customers;
        private bool _isEnabled;
        private bool _isLoading;
        private GrpcCustomer _selectedCustomer;

        public ListViewModel(
            CustomerService.CustomerServiceClient grpc,
            IRegionManager regionManager
        )
        {
            _grpc = grpc;
            _regionManager = regionManager;

            LoadData = new DelegateCommand(
                async () =>
                {
                    IsLoading = true;
                    await LoadGrpcData();
                    IsLoading = false;
                },
                () => !IsLoading
            );

            NewCustomer = new DelegateCommand(
                () => _regionManager.RequestNavigate(
                    RegionNames.Content,
                    nameof(Card)
                )
            );

            DataGridDoubleClick = new DelegateCommand(
                NavigateToCustomerCard,
                () => !IsLoading
            );
        }

        public DelegateCommand NewCustomer { get; }
        public DelegateCommand LoadData { get; }
        public DelegateCommand DataGridDoubleClick { get; }

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

        public GrpcCustomer SelectedCustomer
        {
            get => _selectedCustomer;
            set =>
                SetProperty(
                    ref _selectedCustomer,
                    value
                );
        }

        private void NavigateToCustomerCard()
        {
            if (SelectedCustomer is null)
            {
                return;
            }

            var navigationParams = new NavigationParameters
            {
                {
                    "Id", SelectedCustomer.Id
                }
            };

            _regionManager.RequestNavigate(
                RegionNames.Content,
                nameof(Card),
                navigationParams
            );
        }

        private async Task LoadGrpcData()
        {
            var customers = await _grpc.ListAsync(
                new CustomerRequest()
            );

            await Task.Delay(
                5_000
            );

            Customers = customers.Customer.ToList();
        }
    }
}
