using Library.Entities;
using Library.Enums;
using Lib = Library.Entities.LibraryEntitie;

namespace UnitTests
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeLibrary_WhenAddressIsValid()
        {
            // Arrange
            string address = "123 Library St";

            // Act
            Lib library = new Lib(address);

            // Assert
            Assert.AreEqual(address, library.Address);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Constructor_ShouldThrowArgumentException_WhenAddressIsNullOrEmpty(
            string address
        )
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Lib(address));
        }

        [TestMethod]
        public void GetPrintedBooks_ShouldReturnOnlyPrintedBooks()
        {
            // Arrange
            Author author = new Author("Author", "LastName", "description");

            PrintedBook printedBook1 = new PrintedBook(
                "Printed Book 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890"
            );

            PrintedBook printedBook2 = new PrintedBook(
                "Printed Book 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9780987654321"
            );

            EBook eBook1 = new EBook(
                "EBook Title 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890",
                "http://example.com/preview"
            );

            EBook eBook2 = new EBook(
                "EBook Title 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9780987654321",
                "http://example.com/preview"
            );

            Lib library = new Lib("Address");
            library.AddBooks(printedBook1);
            library.AddBooks(printedBook2);
            library.AddBooks(eBook1);
            library.AddBooks(eBook2);

            // Act
            IEnumerable<PrintedBook> printedBooks = library.GetPrintedBooks();

            // Assert
            Assert.AreEqual(2, printedBooks.Count());
            Assert.IsTrue(printedBooks.All(book => book is PrintedBook));
        }

        [TestMethod]
        public void GetPrintedBooks_ShouldReturnOnlyPrintedBooksWithAvailableCopies()
        {
            // Arrange
            Author author = new Author("Author", "LastName", "description");

            PrintedBook printedBook1 = new PrintedBook(
                "Printed Book 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890"
            );
            printedBook1.AvailableCopies = 5;

            PrintedBook printedBook2 = new PrintedBook(
                "Printed Book 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9780987654321"
            );
            printedBook2.AvailableCopies = 0;

            Lib library = new Lib("Address");
            library.AddBooks(printedBook1);
            library.AddBooks(printedBook2);

            // Act
            IEnumerable<PrintedBook> availablePrintedBooks = library.GetPrintedBooks(true); // Only books with available copies
            IEnumerable<PrintedBook> unavailablePrintedBooks = library.GetPrintedBooks(false); // Only books with 0 copies

            // Assert
            Assert.AreEqual(1, availablePrintedBooks.Count());
            Assert.AreEqual(1, unavailablePrintedBooks.Count());
            Assert.IsTrue(availablePrintedBooks.All(book => book.AvailableCopies > 0));
            Assert.IsTrue(unavailablePrintedBooks.All(book => book.AvailableCopies == 0));
        }

        [TestMethod]
        public void GetEBooks_ShouldReturnOnlyEBooks()
        {
            // Arrange
            Author author = new Author("Author", "LastName", "description");

            EBook eBook1 = new EBook(
                "EBook Title 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890",
                "http://example.com/preview"
            );

            EBook eBook2 = new EBook(
                "EBook Title 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9780987654321",
                "http://example.com/preview"
            );

            PrintedBook printedBook1 = new PrintedBook(
                "Printed Book 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890"
            );

            PrintedBook printedBook2 = new PrintedBook(
                "Printed Book 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9780987654321"
            );

            Lib library = new Lib("Address");
            library.AddBooks(printedBook1);
            library.AddBooks(printedBook2);
            library.AddBooks(eBook1);
            library.AddBooks(eBook2);

            // Act
            IEnumerable<EBook> eBooks = library.GetEBooks();

            // Assert
            Assert.AreEqual(2, eBooks.Count());
            Assert.IsTrue(eBooks.All(book => book is EBook));
        }

        [TestMethod]
        public void RemoveBook_ShouldRemoveExistingBookFromBooksList()
        {
            // Arrange
            Author author = new Author("Author", "LastName", "Biography");

            // Updated EBook constructor usage
            EBook eBook = new EBook(
                "EBook Title 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890",
                "http://example.com/preview"
            );

            PrintedBook printedBook = new PrintedBook(
                "Printed Book Title 1",
                "desciption",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890"
            );

            Lib library = new Lib("123 Library St");

            // Adding books
            library.AddBooks(printedBook);
            library.AddBooks(eBook);

            // Act
            bool resultPrintedBook = library.RemoveBook(printedBook);
            bool resultEBook = library.RemoveBook(eBook);

            // Assert
            Assert.IsTrue(resultPrintedBook);
            Assert.IsTrue(resultEBook);
            Assert.IsFalse(library.Contains(printedBook));
            Assert.IsFalse(library.Contains(eBook));
        }

        [TestMethod]
        public void RemoveBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            Author author = new Author("Author", "LastName", "Biography");

            EBook eBook = new EBook(
                "Nonexistent EBook Title",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "0000000000000",
                "http://example.com/preview"
            );

            PrintedBook printedBook = new PrintedBook(
                "Nonexistent Book Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "0000000000000"
            );

            Lib library = new Lib("123 Library St");

            // Adding an existing book in the library
            PrintedBook existingPrintedBook = new PrintedBook(
                "Existing Book Title",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author },
                "9781234567890"
            );
            library.AddBooks(existingPrintedBook);

            // Act
            bool resultPrintedBook = library.RemoveBook(printedBook);
            bool resultEBook = library.RemoveBook(eBook);

            // Assert
            Assert.IsFalse(resultPrintedBook);
            Assert.IsFalse(resultEBook);
            Assert.IsTrue(library.Contains(existingPrintedBook));
        }

        [TestMethod]
        public void GetBooksByAuthor_ShouldReturnBooksByGivenAuthor()
        {
            // Arrange
            Author author1 = new Author("Author", "FirstName", "Biography");
            Author author2 = new Author("Other", "Author", "Biography");

            PrintedBook printedBook1 = new PrintedBook(
                "Printed Book 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author1 },
                "9781234567890"
            );

            PrintedBook printedBook2 = new PrintedBook(
                "Printed Book 2",
                "Description",
                new List<BookGenre> { BookGenre.Science },
                new List<Author> { author1 },
                "9780987654321"
            );

            EBook eBook1 = new EBook(
                "EBook Title 1",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author2 },
                "9781234567890",
                "http://example.com/preview"
            );

            Lib library = new Lib("Address");
            library.AddBooks(printedBook1, printedBook2, eBook1);

            // Act
            var booksByAuthor1 = library.GetBooksByAuthor(author1);

            // Assert
            Assert.AreEqual(2, booksByAuthor1.Count());
            Assert.IsTrue(booksByAuthor1.All(book => book.Authors.Contains(author1)));

            // Act for author2
            var booksByAuthor2 = library.GetBooksByAuthor(author2);

            // Assert
            Assert.AreEqual(1, booksByAuthor2.Count());
            Assert.IsTrue(booksByAuthor2.All(book => book.Authors.Contains(author2)));
        }

        [TestMethod]
        public void GetBooksByAuthor_ShouldThrowArgumentNullException_WhenAuthorIsNull()
        {
            // Arrange
            Lib library = new Lib("Address");

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => library.GetBooksByAuthor(null));
        }
    }
}
