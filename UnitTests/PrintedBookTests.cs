using System.Xml.Linq;
using Library.Entities;
using Library.Enums;

namespace UnitTests
{
    [TestClass]
    public class PrintedBookTests
    {
        [TestMethod]
        [DataRow(
            "The Great Gatsby",
            "A classic novel set in the Jazz Age.",
            new[] { BookGenre.Fantasy },
            "9780743273565",
            "Hardcover"
        )]
        [DataRow(
            "1984",
            "A dystopian novel about a totalitarian regime.",
            new[] { BookGenre.Science, BookGenre.Mystery },
            "9780451524935",
            "Paperback"
        )]
        public void Constructor_ShouldInitializePrintedBook_WithValidData(
            string title,
            string description,
            BookGenre[] genres,
            string isbn,
            string coverType
        )
        {
            // Arrange
            List<Author> authors = new([new Author("name", "LastName", "biograpy")]);
            List<BookGenre> genreList = genres.ToList();

            // Act
            PrintedBook printedBook = new PrintedBook(title, description, genreList, authors, isbn);
            printedBook.CoverType = coverType;

            // Assert
            Assert.AreEqual(title, printedBook.Title);
            Assert.AreEqual(description, printedBook.Description);
            CollectionAssert.AreEqual(genreList, printedBook.Genres);
            CollectionAssert.AreEqual(authors, printedBook.Authors);
            Assert.AreEqual(isbn, printedBook.ISBN);
            Assert.AreEqual(coverType, printedBook.CoverType);
        }

        [TestMethod]
        [DataRow(
            null,
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            "Hardcover"
        )] // title null
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            null,
            "9780743273565",
            (uint)10,
            "Hardcover"
        )] // authorNames null
        [DataRow(
            "Title",
            null,
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            "Hardcover"
        )] // description null
        [DataRow(
            "Title",
            "Description",
            null,
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            "Hardcover"
        )] // genres null
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            null,
            (uint)10,
            "Hardcover"
        )] // isbn null
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "97807432735-65",
            (uint)10,
            "Hardcover"
        )] // isbn invalid
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            ""
        )] // coverType empty
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            "1234"
        )] // coverType with numbers
        [DataRow(
            "Title",
            "Description",
            new[] { BookGenre.Fantasy },
            new[] { "Author" },
            "9780743273565",
            (uint)10,
            null
        )] // coverType null
        public void Constructor_ShouldThrowArgumentException_WithInvalidAvailableCopiesOrCoverType(
            string title,
            string description,
            BookGenre[] genres,
            string[] authorNames,
            string isbn,
            uint availableCopies,
            string coverType
        )
        {
            // Arrange
            List<Author>? authors = authorNames
                ?.Select(name => new Author(name, "LastName", "biography"))
                .ToList();
            List<BookGenre>? genreList = genres?.ToList();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                PrintedBook printedBook = new PrintedBook(
                    title,
                    description,
                    genreList,
                    authors,
                    isbn,
                    availableCopies,
                    coverType
                );
            });
        }

        [TestMethod]
        [DataRow("Book Title", (uint)5, (uint)4)]
        [DataRow("Another Book", (uint)3, (uint)2)]
        [DataRow("Classic Book", (uint)10, (uint)9)]
        public void GetBook_ShouldDecreaseAvailableCopies_WhenValidData(
            string title,
            uint availableCopies,
            uint expectedAvailableCopiesAfterTake
        )
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                title,
                "Description",
                genres,
                authors,
                "9780743273565",
                availableCopies,
                "Hardcover"
            );
            Visitor visitor = new Visitor("John Doe");

            // Act
            printedBook.GetBook(visitor);

            // Assert
            Assert.AreEqual(expectedAvailableCopiesAfterTake, printedBook.AvailableCopies);
            CollectionAssert.Contains(visitor.ToList(), printedBook);
        }

        [TestMethod]
        public void GetBook_ShouldThrowExceptions_WhenInvalidData()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                "Book Title",
                "Description",
                genres,
                authors,
                "9780743273565",
                5,
                "Hardcover"
            );

            Visitor visitor = null;

            Assert.ThrowsException<ArgumentNullException>(() => printedBook.GetBook(visitor));
        }

        [TestMethod]
        [DataRow("Book Title", (uint)5, (uint)5)]
        [DataRow("Another Book", (uint)3, (uint)3)]
        [DataRow("Classic Book", (uint)10, (uint)10)]
        public void ReturnBook_ShouldIncreaseAvailableCopies_WhenValidData(
            string title,
            uint availableCopies,
            uint expectedAvailableCopiesAfterReturn
        )
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                title,
                "Description",
                genres,
                authors,
                "9780743273565",
                availableCopies,
                "Hardcover"
            );
            Visitor visitor = new Visitor("John Doe");

            // Act
            printedBook.GetBook(visitor);
            printedBook.ReturnBook(visitor);

            // Assert
            Assert.AreEqual(expectedAvailableCopiesAfterReturn, printedBook.AvailableCopies);
            CollectionAssert.DoesNotContain(visitor.ToList(), printedBook);
        }

        [TestMethod]
        public void ReturnBook_ShouldThrowArgumentNullException_WhenVisitorIsNull()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                "Book Title",
                "Description",
                genres,
                authors,
                "9780743273565",
                5,
                "Hardcover"
            );

            Visitor visitor = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => printedBook.ReturnBook(visitor));
        }

        [TestMethod]
        [DataRow(
            "Book Title",
            (uint)5,
            "Hardcover",
            "Title: Book Title\nGenre: Fantasy\nAuthors: FirstName LastName\nISBN: 9780743273565\nAvailable Copies: 5\nCover Type: Hardcover"
        )]
        [DataRow(
            "Another Book",
            (uint)3,
            "Paperback",
            "Title: Another Book\nGenre: Fantasy\nAuthors: FirstName LastName\nISBN: 9780743273565\nAvailable Copies: 3\nCover Type: Paperback"
        )]
        [DataRow(
            "Classic Book",
            (uint)10,
            "Hardcover",
            "Title: Classic Book\nGenre: Fantasy\nAuthors: FirstName LastName\nISBN: 9780743273565\nAvailable Copies: 10\nCover Type: Hardcover"
        )]
        public void Details_ShouldReturnCorrectDetails_WhenValidData(
            string title,
            uint availableCopies,
            string coverType,
            string expectedDetails
        )
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                title,
                "Description",
                genres,
                authors,
                "9780743273565",
                availableCopies,
                coverType
            );

            // Act
            string actualDetails = printedBook.Details;

            // Assert
            Assert.AreEqual(expectedDetails, actualDetails);
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectDetails_WhenAvailableCopiesIsZero()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                "Out of Stock Book",
                "Description",
                genres,
                authors,
                "9780743273565",
                0,
                "Paperback"
            );

            // Act
            string actualDetails = printedBook.Details;

            // Assert
            string expectedDetails =
                "Title: Out of Stock Book\nGenre: Fantasy\nAuthors: FirstName LastName\nISBN: 9780743273565\nAvailable Copies: 0\nCover Type: Paperback";
            Assert.AreEqual(expectedDetails, actualDetails);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectString_WhenValidData()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            PrintedBook printedBook = new PrintedBook(
                "Book Title",
                "Description",
                genres,
                authors,
                "9780743273565",
                5, // Available copies = 5
                "Hardcover"
            );

            // Act
            string actualString = printedBook.ToString();

            // Assert
            string expectedString =
                "Book Title Fantasy FirstName LastName 9780743273565 5 Hardcover";
            Assert.AreEqual(expectedString, actualString);
        }
    }
}
