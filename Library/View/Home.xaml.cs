using Library.Entities;
using Library.Service;
using Library.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        private HomeViewModel ViewModel { get; set; }

        public Home()
        {
            this.InitializeComponent();

            ViewModel = new HomeViewModel();

            this.DataContext = ViewModel;
        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Book selectedBook)
                NavigationService.NavigateToPageStatic("BookDetailsPage", selectedBook);
        }

        private async void OnOpenAddDialog(object sender, RoutedEventArgs e)
        {
            AddDialogBook dialog = new AddDialogBook()
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            };

            dialog.BookAdded += ViewModel.AddOptimisticBook;

            var result = await dialog.ShowAsync();
        }
    }
}
