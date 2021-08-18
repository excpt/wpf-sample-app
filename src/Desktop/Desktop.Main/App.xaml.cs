namespace Sample.App.Desktop.Main
{
    using System.Windows;

    using Customer.Module;

    using Prism.Ioc;
    using Prism.Modularity;

    public partial class App
    {
        protected override void RegisterTypes(
            IContainerRegistry containerRegistry
        ) { }

        protected override void ConfigureModuleCatalog(
            IModuleCatalog moduleCatalog
        )
        {
            moduleCatalog.AddModule<CustomerModule>();
        }

        protected override Window CreateShell() =>
            Container.Resolve<Shell.Shell>();
    }
}
