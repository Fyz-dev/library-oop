using Library.Entities;
using Library.Service;
using Library.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisitorPage : Page
    {
        private VisitorPageViewModel ViewModel => DataContext as VisitorPageViewModel;

        public VisitorPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not Visitor visitor)
                return;

            var viewModel = new VisitorPageViewModel(visitor);
            DataContext = viewModel;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBackStatic();
        }
    }
}
