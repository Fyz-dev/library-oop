using System;
using System.Collections.Generic;
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
    public sealed partial class BookDetailsPage : Page
    {
        private BookDetailsPageViewModel ViewModel => DataContext as BookDetailsPageViewModel;

        public BookDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not Book book)
                return;

            var viewModel = new BookDetailsPageViewModel(book);
            DataContext = viewModel;

            var bookTypeToButtonContent= new Dictionary<Type, string>
            {
                { typeof(EBook), "Buy a book" },
                { typeof(PrintedBook), "Reserve a book" },
            };

            if (bookTypeToButtonContent.TryGetValue(book.GetType(), out var content))
                ButtonGetBook.Content = content;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBackStatic();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RemoveBook();
            NavigationService.GoBackStatic();
        }

        private async void ButtonGetBook_Click(object sender, RoutedEventArgs e)
        {
            GetBookDialog dialog = new GetBookDialog(ViewModel.Book)
            {
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            };

            var result = await dialog.ShowAsync();

            //if (result == ContentDialogResult.Primary)
            //    ViewModel.Books.Add(dialog.NewBook);
        }
    }
}
