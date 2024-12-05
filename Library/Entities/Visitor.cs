using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

namespace Library.Entities
{
    public class Visitor : IEnumerable<Book>
    {
        private string _name;
        private List<Book> _takenBooks;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");

                if (value.Length > 25)
                    throw new ArgumentException("Name cannot exceed 25 characters.");

                if (!Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("Name can only contain English letters and spaces.");

                _name = value;
            }
        }

        public Visitor(string name)
        {
            Name = name;
            _takenBooks = new List<Book>();
        }

        public void TakeBook(Book book)
        {
            if (_takenBooks.Contains(book))
                throw new InvalidOperationException("This book has already been taken by the visitor.");

            _takenBooks.Add(book);
        }

        public void ReturnBook(Book book)
        {
            if (!_takenBooks.Contains(book))
                throw new InvalidOperationException("This book has not been taken by the visitor.");

            _takenBooks.Remove(book);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return _takenBooks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
