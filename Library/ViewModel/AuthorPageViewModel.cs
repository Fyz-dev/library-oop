using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Library.Entities;
using Library.Service;

namespace Library.ViewModel
{
    public class AuthorPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Book> Books { get; set; }
        public Author Author { get; set; }

        public AuthorPageViewModel(Author author)
        {
            Author = author;
            Books = new ObservableCollection<Book>(DataService.Library.GetBooksByAuthor(Author).Order());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
