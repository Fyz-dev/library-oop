using Library.Entities;
using Library.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.ViewModel
{
    public class AuthorsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Author> Authors { get; }

        public AuthorsViewModel()
        {
            Authors = DataService.Library.Authors;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
