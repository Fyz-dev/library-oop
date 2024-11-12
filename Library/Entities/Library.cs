using Library.Interface;
using System;
using System.Collections.Generic;

namespace Library.Entities
{
    public class Library
    {
        private List<IBook> books;

        public List<IBook> Books
        {
            get => throw new NotImplementedException();
        }

        public string Address
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int BookCount => throw new NotImplementedException();

        public Library(string address)
        {
            throw new NotImplementedException();
        }

        public void AddBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public List<IBook> FindBooks(string str)
        {
            throw new NotImplementedException();
        }

        public List<PrintedBook> GetPrintedBooks()
        {
            throw new NotImplementedException();
        }

        public List<PrintedBook> GetPrintedBooks(bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public List<EBook> GetEBooks()
        {
            throw new NotImplementedException();
        }

        public bool RemoveBook(IBook book)
        {
            throw new NotImplementedException();
        }
    }
}
