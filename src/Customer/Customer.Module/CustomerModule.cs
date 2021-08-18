namespace Sample.App.Customer.Module
{
    using Grpc.Net.Client;

    using Kernel.Desktop;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using Remote.CustomerService;

    public class CustomerModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var channel = GrpcChannel.ForAddress(
                "https://localhost:5001"
            );

            var client = new CustomerService.CustomerServiceClient(
                channel
            );

            containerRegistry.RegisterInstance(
                client
            );
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion<List.List>(
                RegionNames.Content
            );
        }
    }
}
