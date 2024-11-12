using Library.Enums;
using Library.Interface;
using Library.Entities;
using Lib = Library.Entities.Library;

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
        public void Constructor_ShouldThrowArgumentException_WhenAddressIsNullOrEmpty(string address)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Lib(address));
        }

        [TestMethod]
        [DataRow("Book Title 1", 1, "Book Title 1")]
        [DataRow("Author", 2, "Book Title 1", "Book Title 2")]
        [DataRow("FANTASY", 1, "Book Title 1")] 
        [DataRow("Publisher", 2, "Book Title 1", "Book Title 2")]  
        [DataRow("Nonexistent Title", 0)]
        [DataRow("Nonexistent Author", 0)]
        [DataRow("SCIENCE", 1, "Book Title 2")] 
        [DataRow("Book Title", 2, "Book Title 1", "Book Title 2")]
        public void FindBooks_ShouldReturnBooksBasedOnSearchCriteria(string searchQuery, int expectedCount, params string[] expectedTitles)
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");

            PrintedBook printedBook1 = new PrintedBook("Book Title 1", BookGenre.FANTASY, author, publisher, "978-1234567890");
            PrintedBook printedBook2 = new PrintedBook("Book Title 2", BookGenre.SCIENCE, author, publisher, "978-0987654321");
            EBook eBook1 = new EBook("EBook Title 1", BookGenre.FANTASY, author);
            EBook eBook2 = new EBook("EBook Title 2", BookGenre.SCIENCE, author);

            Lib library = new Lib("123 Library St");

            library.AddBook(printedBook1);
            library.AddBook(printedBook2);
            library.AddBook(eBook1);
            library.AddBook(eBook2);

            // Act
            List<IBook> foundBooks = library.FindBooks(searchQuery);

            // Assert
            Assert.AreEqual(expectedCount, foundBooks.Count);

            if (expectedCount > 0)
            {
                List<string> foundTitles = foundBooks.Select(b => b.Title).ToList();
                foreach (var expectedTitle in expectedTitles)
                    Assert.IsTrue(foundTitles.Contains(expectedTitle), $"Expected book title '{expectedTitle}' was not found.");
            }
        }

        [TestMethod]
        public void GetPrintedBooks_ShouldReturnOnlyPrintedBooks()
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");
            PrintedBook printedBook1 = new PrintedBook("Printed Book 1", BookGenre.FANTASY, author, publisher, "978-1234567890", 5);
            PrintedBook printedBook2 = new PrintedBook("Printed Book 2", BookGenre.SCIENCE, author, publisher, "978-0987654321", 0);
            EBook eBook1 = new EBook("EBook Title 1", BookGenre.FANTASY, author);
            EBook eBook2 = new EBook("EBook Title 2", BookGenre.SCIENCE, author);

            Lib library = new Lib("Address");
            library.Books.Add(printedBook1);
            library.Books.Add(printedBook2);
            library.Books.Add(eBook1);
            library.Books.Add(eBook2);

            // Act
            List<PrintedBook> printedBooks = library.GetPrintedBooks();

            // Assert
            Assert.AreEqual(2, printedBooks.Count);
            Assert.IsTrue(printedBooks.All(book => book is PrintedBook));
        }

        [TestMethod]
        public void GetPrintedBooks_ShouldReturnOnlyPrintedBooksWithAvailableCopies_0()
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");
            PrintedBook printedBook1 = new PrintedBook("Printed Book 1", BookGenre.FANTASY, author, publisher, "978-1234567890", 5);
            PrintedBook printedBook2 = new PrintedBook("Printed Book 2", BookGenre.SCIENCE, author, publisher, "978-0987654321", 0);

            Lib library = new Lib("Address");
            library.Books.Add(printedBook1);
            library.Books.Add(printedBook2);

            // Act
            List<PrintedBook> availablePrintedBooks = library.GetPrintedBooks(true);
            List<PrintedBook> unavailablePrintedBooks = library.GetPrintedBooks(false);

            // Assert
            Assert.AreEqual(1, availablePrintedBooks.Count);
            Assert.AreEqual(1, unavailablePrintedBooks.Count);
            Assert.IsTrue(availablePrintedBooks.All(book => book.AvailableCopies > 0));
            Assert.IsTrue(unavailablePrintedBooks.All(book => book.AvailableCopies == 0));
        }

        [TestMethod]
        public void GetEBooks_ShouldReturnOnlyEBooks()
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");
            PrintedBook printedBook1 = new PrintedBook("Printed Book 1", BookGenre.FANTASY, author, publisher, "978-1234567890", 5);
            PrintedBook printedBook2 = new PrintedBook("Printed Book 2", BookGenre.SCIENCE, author, publisher, "978-0987654321", 0);
            EBook eBook1 = new EBook("EBook Title 1", BookGenre.FANTASY, author);
            EBook eBook2 = new EBook("EBook Title 2", BookGenre.SCIENCE, author);

            Lib library = new Lib("Address");
            library.Books.Add(printedBook1);
            library.Books.Add(printedBook2);
            library.Books.Add(eBook1);
            library.Books.Add(eBook2);

            // Act
            List<EBook> eBooks = library.GetEBooks();

            // Assert
            Assert.AreEqual(2, eBooks.Count);
            Assert.IsTrue(eBooks.All(book => book is EBook));
        }

        [TestMethod]
        public void RemoveBook_ShouldRemoveExistingBookFromBooksList()
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");

            PrintedBook printedBook = new PrintedBook("Book Title 1", BookGenre.FANTASY, author, publisher, "978-1234567890");
            EBook eBook = new EBook("EBook Title 1", BookGenre.FANTASY, author);

            Lib library = new Lib("123 Library St");

            library.AddBook(printedBook);
            library.AddBook(eBook);

            // Act
            bool resultPrintedBook = library.RemoveBook(printedBook);
            bool resultEBook = library.RemoveBook(eBook);

            // Assert
            Assert.IsTrue(resultPrintedBook);
            Assert.IsTrue(resultEBook);
            Assert.IsFalse(library.Books.Contains(printedBook));
            Assert.IsFalse(library.Books.Contains(eBook));
        }

        [TestMethod]
        public void RemoveBook_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            Publisher publisher = new Publisher("Publisher", "Publisher Address");

            PrintedBook printedBook = new PrintedBook("Nonexistent Book Title", BookGenre.FANTASY, author, publisher, "000-0000000000");
            EBook eBook = new EBook("Nonexistent EBook Title", BookGenre.FANTASY, author);

            Lib library = new Lib("123 Library St");

            PrintedBook existingPrintedBook = new PrintedBook("Existing Book Title", BookGenre.SCIENCE, author, publisher, "978-1234567890");
            library.AddBook(new PrintedBook("Existing Book Title", BookGenre.SCIENCE, author, publisher, "978-1234567890"));

            // Act
            bool resultPrintedBook = library.RemoveBook(printedBook);
            bool resultEBook = library.RemoveBook(eBook);

            // Assert
            Assert.IsFalse(resultPrintedBook);
            Assert.IsFalse(resultEBook);
            Assert.IsTrue(library.Books.Contains(existingPrintedBook));
        }
    }
}
