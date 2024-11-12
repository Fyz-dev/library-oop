using Library.Entities;
using Library.Enums;
using System.Collections.Generic;

namespace Library.Interface
{
    public interface IBook
    {
        string Title { get; set; }

        BookGenre Genre { get; set; }

        Author Author { get; set; }
    }
}
