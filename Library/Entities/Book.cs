using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Library.Enums;
using Library.Interface;

namespace Library.Entities
{
    public abstract class Book : INotifyPropertyChanged, IBook, IComparable<Book>
    {
        private string _title;
        private string _isbn;
        private string _description;
        private List<BookGenre> _genres;
        private List<Author> _authors;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be null or empty.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s,.#-]+$"))
                    throw new ArgumentException(
                        "Title can only contain English letters, numbers, spaces, commas, periods, and hyphens."
                    );

                _title = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Description cannot be null or empty.");

                if (value.Length > 500)
                    throw new ArgumentException("Description cannot exceed 500 characters.");

                _description = value;
            }
        }

        public List<BookGenre> Genres
        {
            get => _genres;
            set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentException("At least one genre must be selected.");
                _genres = value;
            }
        }

        public List<Author> Authors
        {
            get => _authors;
            set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentException("At least one author must be selected.");
                _authors = value;
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN cannot be null or empty.");

                if (!Regex.IsMatch(value, @"^\d{13}$"))
                    throw new ArgumentException("ISBN must be exactly 13 digits.");

                _isbn = value;
            }
        }

        public virtual string Details =>
                    $"Title: {Title}\n" +
                    $"Genre: {string.Join(", ", Genres)}\n" +
                    $"Authors: {string.Join(", ", Authors.Select(a => a.FullName))}\n" +
                    $"ISBN: {ISBN}";

        protected Book(string title, string description, List<BookGenre> genres, List<Author> authors, string isbn)
        {
            Title = title;
            Description = description;
            Genres = genres;
            Authors = authors;
            ISBN = isbn;
        }

        public abstract void GetBook(Visitor visitor);

        public int CompareTo(Book other)
        {
            if (other == null) return 1;

            return string.Compare(this.Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
