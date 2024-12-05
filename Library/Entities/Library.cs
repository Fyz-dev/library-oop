using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library.Entities
{
    public class LibraryEntitie : IEnumerable<Book>
    {
        private ObservableCollection<Book> _books = new();
        private ObservableCollection<Author> _authors = new();
        private ObservableCollection<Visitor> _visitors = new();

        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Address cannot be null or empty.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s,.-]+$"))
                    throw new ArgumentException(
                        "Address contains invalid characters. Only English letters, numbers, spaces, commas, periods, and dashes are allowed."
                    );

                _address = value;
            }
        }

        public ObservableCollection<Author> Authors => _authors;
        public ObservableCollection<Visitor> Visitors => _visitors;

        public LibraryEntitie(string address)
        {
            Address = address;
        }

        public void AddBooks(params Book[] books)
        {
            if (books == null || books.Length == 0)
                throw new ArgumentException("At least one book must be provided.", nameof(books));

            foreach (var book in books)
                _books.Add(book);
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author), "Author cannot be null.");

            return _books.Where(book => book.Authors.Contains(author));
        }

        public IEnumerable<PrintedBook> GetPrintedBooks() => _books.OfType<PrintedBook>();

        public IEnumerable<PrintedBook> GetPrintedBooks(bool isAvailable)
        {
            return _books
                .OfType<PrintedBook>()
                .Where(pb => isAvailable == (pb.AvailableCopies > 0));
        }

        public IEnumerable<EBook> GetEBooks() => _books.OfType<EBook>();

        public bool RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            foreach (var visitor in Visitors)
                if (visitor.Contains(book))
                    visitor.ReturnBook(book);

            return _books.Remove(book);
        }
        public IEnumerator<Book> GetEnumerator()
        {
            return _books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
