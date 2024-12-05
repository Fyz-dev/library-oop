using Library.ViewModel;
using Microsoft.UI.Xaml.Controls;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDialogAuthor : ContentDialog
    {
        AddDialogAuthorViewModel ViewModel { get; set; }

        public AddDialogAuthor()
        {
            this.InitializeComponent();

            ViewModel = new AddDialogAuthorViewModel();

            this.DataContext = ViewModel;
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.None)
                return;

            if (!ViewModel.AddAuthor())
                args.Cancel = true;
        }
    }
}
