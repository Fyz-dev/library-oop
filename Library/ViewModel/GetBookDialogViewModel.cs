using Library.Entities;
using Library.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Library.ViewModel
{
    public class GetBookDialogViewModel: INotifyPropertyChanged
    {
        private string _errorMessage;

        private Book _book;
        public ObservableCollection<Visitor> Visitors { get; set; }

        public Visitor SelectedVisitor { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public GetBookDialogViewModel(Book book) 
        {
            _book = book;
            Visitors = new ObservableCollection<Visitor>(DataService.Library.Visitors.Where(visitor => !visitor.Contains(book)));
        }

        public bool GetBook()
        {
            try
            {
                _book.GetBook(SelectedVisitor);

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
