using Library.Entities;
using Library.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.ViewModel
{
    public class BookDetailsPageViewModel : INotifyPropertyChanged
    {
        public Book Book { get; set; }

        public BookDetailsPageViewModel(Book book)
        {
            Book = book;
        }

        public void RemoveBook()
        {
            DataService.Library.RemoveBook(Book);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
