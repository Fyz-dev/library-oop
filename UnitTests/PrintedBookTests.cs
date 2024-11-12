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
            BookGenre.FANTASY,
            "F. Scott Fitzgerald",
            "Scribner",
            "978-0743273565",
            "Hardcover"
        )]
        [DataRow(
            "1984",
            BookGenre.SCIENCE,
            "George Orwell",
            "Secker & Warburg",
            "978-0451524935",
            "Paperback"
        )]
        public void Constructor_ShouldInitializePrintedBook_WithValidData(
            string title,
            BookGenre genre,
            string authorName,
            string publisherName,
            string isbn,
            string coverType
        )
        {
            // Arrange
            Author author = new Author(authorName, "LastName");
            Publisher publisher = new Publisher(publisherName, "Publisher Address");

            // Act
            PrintedBook printedBook = new PrintedBook(title, genre, author, publisher, isbn);
            printedBook.CoverType = coverType;

            // Assert
            Assert.AreEqual(title, printedBook.Title);
            Assert.AreEqual(genre, printedBook.Genre);
            Assert.AreEqual(author, printedBook.Author);
            Assert.AreEqual(publisher, printedBook.Publisher);
            Assert.AreEqual(isbn, printedBook.ISBN);
            Assert.AreEqual(coverType, printedBook.CoverType);
        }

        [TestMethod]
        [DataRow(null, BookGenre.FANTASY, "Author", "Publisher", "978-0743273565", 0)] // title null
        [DataRow("Title", BookGenre.FANTASY, null, "Publisher", "978-0743273565", 0)] // authorName null
        [DataRow("Title", BookGenre.FANTASY, "Author", null, "978-0743273565", 0)] // publisherName null
        [DataRow("Title", BookGenre.FANTASY, "Author", "Publisher", null, 0)] // isbn null
        [DataRow("Title", BookGenre.FANTASY, "Author", "Publisher", "9780743273565", 0)] // isbn invalid
        [DataRow("Title", BookGenre.SCIENCE, "Author", "Publisher", "978-0451524935", -1)] // availableCopies -1
        public void Constructor_ShouldThrowArgumentException_WithInvalidData(
            string title,
            BookGenre genre,
            string authorName,
            string publisherName,
            string isbn,
            int availableCopies
        )
        {
            // Arrange
            Author? author = authorName != null ? new Author(authorName, "LastName") : null;
            Publisher? publisher =
                publisherName != null ? new Publisher(publisherName, "Publisher Address") : null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(
                () => new PrintedBook(title, genre, author, publisher, isbn, (uint)availableCopies)
            );
        }

        [TestMethod]
        [DataRow("978-0743273565")]
        [DataRow("9780451524935")]
        public void ISBN_ShouldAcceptValidFormats(string isbn)
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");

            // Act
            PrintedBook printedBook = new PrintedBook(
                "Title",
                BookGenre.FANTASY,
                author,
                publisher,
                isbn
            );

            // Assert
            Assert.AreEqual(isbn, printedBook.ISBN);
        }

        [TestMethod]
        [DataRow("123-456-789")]
        [DataRow("9780743273565")]
        public void ISBN_ShouldThrowArgumentException_WhenInvalidFormat(string isbn)
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(
                () => new PrintedBook("Title", BookGenre.FANTASY, author, publisher, isbn)
            );
        }
    }
}
