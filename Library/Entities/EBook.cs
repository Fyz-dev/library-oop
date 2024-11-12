using Library.Enums;
using Library.Interface;
using System;

namespace Library.Entities
{
    public class EBook : IBook
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

        public FileFormat FileFormat
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public bool DRMProtection
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string PreviewLink
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public EBook(
            string title,
            BookGenre genre,
            Author author,
            FileFormat fileFormat = FileFormat.OTHER
        )
        {
            throw new NotImplementedException();
        }
    }
}
