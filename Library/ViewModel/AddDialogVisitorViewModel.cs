using Library.Entities;
using Library.Service;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.ViewModel
{
    public class AddDialogVisitorViewModel : INotifyPropertyChanged
    {
        private string _errorMessage;

        public string Name { get; set; }

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

        public AddDialogVisitorViewModel()
        {
        }

        public bool AddVisitor()
        {
            try
            {
                DataService.Library.Visitors.Add(new Visitor(Name));

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
