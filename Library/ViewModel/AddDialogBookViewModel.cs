using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Library.Entities;
using Library.Enums;
using Library.Service;

namespace Library.ViewModel
{
    public class AddDialogBookViewModel : INotifyPropertyChanged
    {
        private string _errorMessage;

        public List<string> BookGenres { get; set; } = new(Enum.GetNames(typeof(BookGenre)));
        public List<string> FileFormats { get; set; } = new(Enum.GetNames(typeof(FileFormat)));
        public ObservableCollection<Author> Authors { get; set; } = DataService.Library.Authors;
        public int SelectedIndexBookType { get; set; } = 0;

        // Books
        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

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

        public uint AvailableCopies { get; set; }

        public string CoverType { get; set; }

        public int SelectedFileFormat { get; set; } = (int)FileFormat.OTHER;

        public bool DRMProtection { get; set; }

        public string PreviewLink { get; set; }

        public AddDialogBookViewModel() { }

        public Book AddBook(List<BookGenre> SelectedGenres, List<Author> SelectedAuthors)
        {
            try
            {
                Book newBook = null;

                //Debug.WriteLine($"DRM: {DRMProtection}");
                //Debug.WriteLine($"FileFormat: {SelectedFileFormat}");

                switch (SelectedIndexBookType)
                {
                    case 0:
                        newBook = new PrintedBook(
                            Title,
                            Description,
                            SelectedGenres,
                            SelectedAuthors,
                            ISBN,
                            AvailableCopies,
                            CoverType
                        );
                        DataService.Library.AddBooks(newBook);

                        break;

                    case 1:
                        newBook = new EBook(
                            Title,
                            Description,
                            SelectedGenres,
                            SelectedAuthors,
                            ISBN,
                            PreviewLink,
                            (FileFormat)SelectedFileFormat
                        )
                        {
                            DRMProtection = DRMProtection,
                        };
                        DataService.Library.AddBooks(newBook);

                        break;
                }

                return newBook;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
