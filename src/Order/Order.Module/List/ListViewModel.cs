namespace Sample.App.Order.Module.List
{
    using Prism.Commands;
    using Prism.Mvvm;

    public class ListViewModel : BindableBase
    {
        public ListViewModel() =>
            LoadData = new DelegateCommand(
                () => { }
                );

        public DelegateCommand LoadData { get; }
    }
}
