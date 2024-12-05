using Library.Entities;
using Library.ViewModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetBookDialog : ContentDialog
    {
        private GetBookDialogViewModel ViewModel { get; set; }

        private Book Book { get; set; }

        public GetBookDialog(Book book)
        {
            this.InitializeComponent();

            ViewModel = new GetBookDialogViewModel(book);
            Book = book;

            this.DataContext = ViewModel;

            var bookTypeToButtonContent = new Dictionary<Type, string>
            {
                { typeof(EBook), "Buy a book" },
                { typeof(PrintedBook), "Reserve a book" },
            };

            if (bookTypeToButtonContent.TryGetValue(book.GetType(), out var content))
                this.PrimaryButtonText = content;
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.None)
                return;

            if (!ViewModel.GetBook())
                args.Cancel = true;
        }
    }
}
