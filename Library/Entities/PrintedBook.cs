using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Enums;

namespace Library.Entities
{
    public class PrintedBook : Book
    {
        private string _coverType;
        private uint _availableCopies;

        public uint AvailableCopies
        {
            get => _availableCopies;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Available copies cannot be negative.");

                _availableCopies = value;
            }
        }

        public string CoverType
        {
            get => _coverType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Cover type cannot be null or empty.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new ArgumentException("Cover type must contain only English letters.");

                _coverType = value;
            }
        }

        public override string Details => $"{base.Details}\nAvailable Copies: {AvailableCopies}\nCover Type: {CoverType}";

        public PrintedBook(
            string title,
            string description,
            List<BookGenre> genres,
            List<Author> authors,
            string isbn,
            uint availableCopies = 0,
            string coverType = "Paperback"
        )
            : base(title, description, genres, authors, isbn)
        {
            ISBN = isbn;
            AvailableCopies = availableCopies;
            CoverType = coverType;
        }

        public override void GetBook(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("Visitor cannot be null.");

            if (AvailableCopies <= 0)
                throw new InvalidOperationException("No available copies of this book.");

            visitor.TakeBook(this);
            AvailableCopies--;

            OnPropertyChanged(nameof(Details));
        }

        public void ReturnBook(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor), "Visitor cannot be null.");

            visitor.ReturnBook(this);
            AvailableCopies++;
        }

        public override string ToString()
        {
            string authors = string.Join(" ", Authors.Select(a => a.FullName));
            string genres = string.Join(" ", Genres);

            return $"{Title} {genres} {authors} {ISBN} {AvailableCopies} {CoverType}";
        }
    }
}
