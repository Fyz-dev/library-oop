﻿using System;
using Library.Enums;
using Library.Interface;

namespace Library.Entities
{
    public class PrintedBook : IBook
    {
        public string Title
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public BookGenre Genre
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Author Author
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Publisher Publisher
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string ISBN
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public uint AvailableCopies
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string CoverType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public PrintedBook(
            string title,
            BookGenre genre,
            Author author,
            Publisher publisher,
            string isbn,
            uint availableCopies = 0
        )
        {
            throw new NotImplementedException();
        }
    }
}
