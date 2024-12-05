using Library.Entities;
using Library.Enums;
using System.Collections.Generic;

namespace Library.Interface
{
    public interface IBook
    {
        string Title { get; set; }

        string Description { get; set; }

        List<BookGenre> Genres { get; set; }

        List<Author> Authors { get; set; }

        public string ISBN { get; set; }
    }
}
