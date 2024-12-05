using Library.Entities;
using Library.Service;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Library.ViewModel
{
    public class AddDialogAuthorViewModel : INotifyPropertyChanged
    {
        private string _errorMessage;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

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

        public AddDialogAuthorViewModel()
        {
        }

        public bool AddAuthor()
        {
            try
            {
                Author author = new Author(FirstName, LastName, Biography);

                DataService.Library.Authors.Add(author);

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
