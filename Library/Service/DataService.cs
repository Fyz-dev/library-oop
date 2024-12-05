using System.Collections.Generic;
using Library.Entities;
using Library.Enums;

namespace Library.Service
{
    public static class DataService
    {
        public static LibraryEntitie Library { get; set; }

        static DataService()
        {
            Library = new LibraryEntitie("test");

            Library.Authors.Add(new Author("John", "Doe", "John is a technology enthusiast and software engineer."));
            Library.Authors.Add(new Author("Emily", "Clark", "Emily specializes in AI research and machine learning."));
            Library.Authors.Add(new Author("Jane", "Smith", "Jane is a novelist and futurist writer."));
            Library.Authors.Add(new Author("Alan", "Turing", "Alan was a mathematician and pioneer in computer science."));
            Library.Authors.Add(new Author("Michael", "Brown", "Michael is a programming expert and tech author."));
            Library.Authors.Add(new Author("Sarah", "Johnson", "Sarah is a software engineer and contributor to open-source projects."));
            Library.Authors.Add(new Author("Thomas", "Cormen", "Thomas is a co-author of the famous textbook on algorithms."));
            Library.Authors.Add(new Author("Charles", "Leiserson", "Charles is a computer science professor and algorithm researcher."));
            Library.Authors.Add(new Author("Ronald", "Rivest", "Ronald is a cryptographer and co-author of the RSA algorithm."));

            Library.Visitors.Add(new Visitor("Alice"));
            Library.Visitors.Add(new Visitor("Gleb"));

            Library.AddBooks(
                new EBook(
                    title: "Digital Revolution",
                    description: "A thrilling journey into the mysteries of the digital age.",
                    genres: new List<BookGenre> { BookGenre.Mystery, BookGenre.Science },
                    authors: new List<Author> { Library.Authors[0], Library.Authors[1] },
                    isbn: "9781234567890",
                    previewLink: "https://example.com/digital-revolution",
                    fileFormat: FileFormat.PDF
                )
                {
                    DRMProtection = true,
                },
                new EBook(
                    title: "The Future of AI",
                    description: "Explore the fantastical possibilities of artificial intelligence.",
                    genres: new List<BookGenre> { BookGenre.Fantasy, BookGenre.Science },
                    authors: new List<Author> { Library.Authors[2], Library.Authors[3] },
                    isbn: "9789876543210",
                    previewLink: "https://example.com/future-of-ai",
                    fileFormat: FileFormat.EBUP
                ),
                new PrintedBook(
                    title: "Mastering C#",
                    description: "An essential guide for mastering the C# programming language.",
                    genres: new List<BookGenre> { BookGenre.Technology, BookGenre.Education },
                    authors: new List<Author> { Library.Authors[4], Library.Authors[5] },
                    isbn: "9781122334455",
                    availableCopies: 5,
                    coverType: "Hardcover"
                ),
                new PrintedBook(
                    title: "Introduction to Algorithms",
                    description: "A comprehensive introduction to algorithms, suitable for all skill levels.",
                    genres: new List<BookGenre>
                    {
                        BookGenre.Science,
                        BookGenre.Education,
                        BookGenre.Romance,
                        BookGenre.Fantasy,
                    },
                    authors: new List<Author>
                    {
                        Library.Authors[6],
                        Library.Authors[7],
                        Library.Authors[8],
                    },
                    isbn: "9780262033848",
                    availableCopies: 3,
                    coverType: "Paperback"
                )
            );
        }
    }
}
