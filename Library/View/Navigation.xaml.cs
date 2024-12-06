using Library.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    public sealed partial class Navigation : UserControl
    {
        private readonly NavigationService _navigationService;

        public Navigation()
        {
            this.InitializeComponent();

            _navigationService = new NavigationService(contentFrame);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = NavView.MenuItems[0];
            _navigationService.NavigateToPage("Home");
        }

        public void OnNavigationViewItemInvoked(
            NavigationView sender,
            NavigationViewItemInvokedEventArgs args
        )
        {
            string pageName = args.InvokedItem.ToString();
            _navigationService.NavigateToPage(pageName);
        }
    }
}
