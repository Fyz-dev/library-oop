using Library.Entities;
using Library.Enums;

namespace UnitTests
{
    [TestClass]
    public class EBookTests
    {
        [TestMethod]
        [DataRow(
            "E-Book Title",
            "A description of the E-Book",
            BookGenre.Fantasy,
            "9781234567890",
            "https://www.example.com",
            FileFormat.PDF
        )]
        [DataRow(
            "The Great E-Book",
            "A science e-book description",
            BookGenre.Science,
            "9781234567891",
            "https://www.example.com",
            FileFormat.EBUP
        )]
        [DataRow(
            "Digital Book",
            "A historical book description",
            BookGenre.Historical,
            "9781234567892",
            "https://www.example.com",
            FileFormat.FB2
        )]
        [DataRow(
            "Tech E-Book",
            "A romance book description",
            BookGenre.Romance,
            "9781234567893",
            "https://www.example.com",
            FileFormat.WORD
        )]
        [DataRow(
            "Learning C#",
            "A technology e-book description",
            BookGenre.Fantasy,
            "9781234567894",
            "https://www.example.com",
            FileFormat.OTHER
        )]
        public void Constructor_ShouldInitializeEBook_WithValidValues(
            string title,
            string description,
            BookGenre genre,
            string isbn,
            string previewLink,
            FileFormat fileFormat
        )
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("Author", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { genre };

            // Act
            EBook eBook = new EBook(
                title,
                description,
                genres,
                authors,
                isbn,
                previewLink,
                fileFormat
            );

            // Assert
            Assert.AreEqual(title, eBook.Title);
            Assert.AreEqual(description, eBook.Description);
            CollectionAssert.AreEqual(genres, eBook.Genres);
            CollectionAssert.AreEqual(authors, eBook.Authors);
            Assert.AreEqual(isbn, eBook.ISBN);
            Assert.AreEqual(previewLink, eBook.PreviewLink);
            Assert.AreEqual(fileFormat, eBook.FileFormat);
            Assert.AreEqual(false, eBook.DRMProtection);
            Assert.AreEqual(0, eBook.PurchasedCount); // Initial purchased count should be 0
        }

        [TestMethod]
        [DataRow(
            "",
            "A description of the E-Book",
            BookGenre.Fantasy,
            "9781234567890",
            "https://www.example.com",
            FileFormat.PDF
        )] // Empty title
        [DataRow(
            null,
            "A description of the E-Book",
            BookGenre.Fantasy,
            "9781234567890",
            "https://www.example.com",
            FileFormat.PDF
        )] // Null title
        [DataRow(
            "E-Book Title",
            "",
            BookGenre.Fantasy,
            "9781234567890",
            "https://www.example.com",
            FileFormat.PDF
        )] // Empty description
        [DataRow(
            "E-Book Title",
            "A description of the E-Book",
            BookGenre.Fantasy,
            "INVALID_ISBN",
            "https://www.example.com",
            FileFormat.PDF
        )] // Invalid ISBN
        [DataRow(
            "E-Book Title",
            "A description of the E-Book",
            BookGenre.Fantasy,
            "9781234567890",
            "",
            FileFormat.PDF
        )] // Empty preview link
        [DataRow(
            "E-Book Title",
            "A description of the E-Book",
            BookGenre.Fantasy,
            "9781234567890",
            "invalid-url",
            FileFormat.PDF
        )] // Invalid preview link
        public void Constructor_ShouldThrowArgumentException_WithInvalidValues(
            string title,
            string description,
            BookGenre genre,
            string isbn,
            string previewLink,
            FileFormat fileFormat
        )
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(
                () =>
                    new EBook(
                        title,
                        description,
                        new List<BookGenre> { genre },
                        new List<Author> { new Author("Author", "LastName", "Biography") },
                        isbn,
                        previewLink,
                        fileFormat
                    )
            );
        }

        [TestMethod]
        [DataRow("invalid-url")] // Invalid URL
        [DataRow("http://")] // Incomplete URL
        [DataRow("www.example.com")] // Missing scheme
        [DataRow("://invalidlink.com")] // Invalid URL structure
        public void ShouldThrowArgumentException_WhenSettingInvalidPreviewLink(string previewLink)
        {
            // Arrange
            Author author = new Author("Author", "LastName", "Biography");
            EBook eBook = new EBook(
                "E-Book Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author> { author },
                "9781234567890",
                "https://www.example.com"
            );

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => eBook.PreviewLink = previewLink);
        }

        [TestMethod]
        [DataRow("E-Book Title", 0)]
        [DataRow("Tech E-Book", 3)]
        [DataRow("Digital Book", 5)]
        public void GetBook_ShouldIncreasePurchasedCount_WhenValidData(
            string title,
            int initialPurchasedCount
        )
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author("FirstName", "LastName", "Biography"),
            };
            List<BookGenre> genres = new List<BookGenre> { BookGenre.Fantasy };
            EBook eBook = new EBook(
                title,
                "Description",
                genres,
                authors,
                "9780743273565",
                "https://www.example.com",
                FileFormat.PDF
            );
            eBook.GetType().GetProperty("PurchasedCount")?.SetValue(eBook, initialPurchasedCount);
            Visitor visitor = new Visitor("John Doe");

            // Act
            eBook.GetBook(visitor);

            // Assert
            Assert.AreEqual(initialPurchasedCount + 1, eBook.PurchasedCount);
            CollectionAssert.Contains(visitor.ToList(), eBook);
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
            EBook eBook = new EBook(
                "E-Book Title",
                "Description",
                genres,
                authors,
                "9780743273565",
                "https://www.example.com",
                FileFormat.PDF
            );

            Visitor visitor = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => eBook.GetBook(visitor));
        }

        [TestMethod]
        public void EBook_Details_ShouldReturnCorrectDetails()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author("John", "Doe", "bio"),
                new Author("Jane", "Smith", "bio"),
            };
            var genres = new List<BookGenre> { BookGenre.Fantasy, BookGenre.Mystery };
            var ebook = new EBook(
                "Sample Title",
                "Sample Description",
                genres,
                authors,
                "1234567890123",
                "https://sample.com/preview"
            )
            {
                DRMProtection = true,
                FileFormat = FileFormat.PDF,
            };

            // Act
            var details = ebook.Details;

            // Assert
            var expectedDetails =
                "Title: Sample Title\n"
                + "Genre: Fantasy, Mystery\n"
                + "Authors: John Doe, Jane Smith\n"
                + "ISBN: 1234567890123\n"
                + "File Format: PDF\n"
                + "DRM Protected: True\n"
                + "Preview Link: https://sample.com/preview\n"
                + "Number of purchased: 0";

            Assert.AreEqual(expectedDetails, details);
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
            EBook eBook = new EBook(
                "E-Book Title",
                "Description of the E-Book",
                genres,
                authors,
                "9781234567890",
                "https://www.example.com",
                FileFormat.PDF
            )
            {
                DRMProtection = true,
            };

            // Act
            string actualString = eBook.ToString();

            // Assert
            string expectedString =
                "E-Book Title Fantasy FirstName LastName 9781234567890 PDF True https://www.example.com";
            Assert.AreEqual(expectedString, actualString);
        }
    }
}
