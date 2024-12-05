using Library.Entities;
using Library.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.ViewModel
{
    public class VisitorsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Visitor> Visitors { get; }


        public VisitorsViewModel()
        {
            Visitors = DataService.Library.Visitors;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
