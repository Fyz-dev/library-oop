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
    public sealed partial class Authors : Page
    {

        private AuthorsViewModel ViewModel { get; set; }

        public Authors()
        {
            this.InitializeComponent();

            ViewModel = new AuthorsViewModel();

            this.DataContext = ViewModel;
        }

        private async void OnOpenAddDialog(object sender, RoutedEventArgs e)
        {
            AddDialogAuthor dialog = new AddDialogAuthor()
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            };

            var result = await dialog.ShowAsync();
        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Author selectedAuthor)
                NavigationService.NavigateToPageStatic("AuthorPage", selectedAuthor);
        }
    }
}
