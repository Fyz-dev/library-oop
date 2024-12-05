using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Library.Entities;

namespace Library.ViewModel
{
    public class VisitorPageViewModel : INotifyPropertyChanged
    {
        public Visitor Visitor { get; set; }

        private Book _selectedPrintedBook;
        public Book SelectedPrintedBook
        {
            get => _selectedPrintedBook;
            set
            {
                if (_selectedPrintedBook != value)
                {
                    _selectedPrintedBook = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Book> PrintedBook { get; set; }

        public ObservableCollection<Book> EBook { get; set; }

        public ICommand ReturnBookCommand { get; set; }

        public VisitorPageViewModel(Visitor visitor)
        {
            Visitor = visitor;

            PrintedBook = new ObservableCollection<Book>(
                Visitor.Where(b => b is PrintedBook).Order()
            );
            EBook = new ObservableCollection<Book>(Visitor.Where(b => b is EBook).Order());

            ReturnBookCommand = new RelayCommand(ReturnBook);
        }

        public void ReturnBook()
        {
            if(SelectedPrintedBook is PrintedBook printedBook)
            {
                printedBook.ReturnBook(Visitor);
                PrintedBook.Remove(printedBook);
                OnPropertyChanged(nameof(PrintedBook));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
