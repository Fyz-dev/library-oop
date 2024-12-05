using System.Linq;
using Library.Entities;
using Library.Enums;
using Library.ViewModel;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDialogBook : ContentDialog
    {
        public delegate void BookAddedEventHandler(Book newBook);

        public event BookAddedEventHandler BookAdded;

        private AddDialogBookViewModel ViewModel { get; set; }

        public AddDialogBook()
        {
            this.InitializeComponent();

            ViewModel = new AddDialogBookViewModel();

            this.DataContext = ViewModel;

            BookTypeComboBox.SelectedIndex = 0;
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.None)
                return;

            Book newBook = ViewModel.AddBook(
                GenreListBox
                    .SelectedItems.Select(item => (BookGenre)GenreListBox.Items.IndexOf(item))
                    .ToList(),
                AuthorListBox.SelectedItems.Cast<Author>().ToList()
            );

            if (newBook == null)
            {
                args.Cancel = true;
                return;
            }

            BookAdded.Invoke(newBook);
        }

        private void ComboBoxConstructor_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            ComboBox comboBox = sender as ComboBox;
            int selectedIndex = comboBox.SelectedIndex;

            ViewModel.Title = string.Empty;
            ViewModel.Description = string.Empty;
            GenreListBox.SelectedItems.Clear();
            AuthorListBox.SelectedItems.Clear();
            ViewModel.ISBN = string.Empty;
            ViewModel.AvailableCopies = 1;
            ViewModel.CoverType = "PaperBack";
            ViewModel.SelectedFileFormat = (int)FileFormat.OTHER;
            ViewModel.DRMProtection = false;
            ViewModel.PreviewLink = string.Empty;
            ViewModel.ErrorMessage = null;

            switch (selectedIndex)
            {
                case 0:
                    GridPrintedBook.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    GridEBook.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    break;

                case 1:
                    GridPrintedBook.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    GridEBook.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    break;
            }
        }
    }
}
