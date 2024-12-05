using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Library.Entities;
using Library.Helpers;
using Library.Service;

namespace Library.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private int _selectedTypeBookIndex;
        private int _selectedSearchCriterionIndex;
        private string _searchQuery = String.Empty;
        private readonly DelayedAction _delayedSearch;

        public ObservableCollection<Book> Books { get; set; }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    _delayedSearch.Restart();
                }
            }
        }

        public int SelectedTypeBookIndex
        {
            get => _selectedTypeBookIndex;
            set
            {
                _selectedTypeBookIndex = value;
                OnPropertyChanged(nameof(SelectedTypeBookIndex));
                PerformSearch();
            }
        }

        public int SelectedSearchCriterionIndex
        {
            get => _selectedSearchCriterionIndex;
            set
            {
                _selectedSearchCriterionIndex = value;
                OnPropertyChanged(nameof(SelectedSearchCriterionIndex));
                PerformSearch();
            }
        }

        public HomeViewModel()
        {
            _delayedSearch = new DelayedAction(TimeSpan.FromMilliseconds(500), PerformSearch);

            Books = new ObservableCollection<Book>(DataService.Library.Order());
        }

        public void AddOptimisticBook(Book book)
        {
            Books.Add(book);
            PerformSearch();
        }

        private void PerformSearch()
        {
            IEnumerable<Book> filteredBooks = SelectedTypeBookIndex switch
            {
                1 => DataService.Library.GetPrintedBooks(), // Printed Books
                2 => DataService.Library.GetPrintedBooks(true), // Printed Books with Available Copies
                3 => DataService.Library.GetEBooks(), // E-Books
                _ => DataService.Library, // All Books
            };

            IEnumerable<Book> resultBooks = SelectedSearchCriterionIndex switch
            {
                1 => filteredBooks.Where(book =>
                    book.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                ), // Search by Title

                2 => filteredBooks.Where(book =>
                    book.Authors.Any(author =>
                       author.FullName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                    )
                ), // Search by Author

                3 => filteredBooks.Where(book =>
                    book.Genres.Any(genre =>
                        genre.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                    )
                ), // Search by Genre

                0 => filteredBooks.Where(book =>
                    book.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                ), // Search All Fields

                _ => filteredBooks,
            };

            Books = new ObservableCollection<Book>(resultBooks.Order());
            OnPropertyChanged(nameof(Books));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
