using Library.Entities;
using Library.Enums;

namespace UnitTests
{
    [TestClass]
    public class EBookTests
    {
        [TestMethod]
        [DataRow("E-Book Title", BookGenre.FANTASY)] 
        [DataRow("The Great E-Book", BookGenre.SCIENCE)]
        [DataRow("Digital Book", BookGenre.HISTORICAL)]
        [DataRow("Tech E-Book", BookGenre.ROMANCE)]
        [DataRow("Learning C#", BookGenre.FANTASY)] 
        public void Constructor_ShouldInitializeEBook_WithValidValues(
            string title, BookGenre genre)
        {
            // Act
            EBook eBook = new EBook(title, genre, new Author("Author", "LastName"));

            // Assert
            Assert.AreEqual(title, eBook.Title);
            Assert.AreEqual(genre, eBook.Genre);
            Assert.AreEqual(false, eBook.DRMProtection);
            Assert.AreEqual(FileFormat.OTHER, eBook.FileFormat);
        }

        [TestMethod]
        [DataRow("", BookGenre.FANTASY)]
        [DataRow("E-Book Title", (BookGenre)999)]
        [DataRow(null, BookGenre.FANTASY)] 
        public void Constructor_ShouldThrowArgumentException_WithInvalidValues(
            string title, BookGenre genre)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new EBook(title, genre, new Author("Author", "LastName")));
        }

        [TestMethod]
        [DataRow("https://www.example.com")]
        [DataRow("https://www.example.com/page1")]
        [DataRow("https://www.example.com/preview?book=123")]
        [DataRow("https://example.com/preview/book/456")]
        public void ShouldSetValidPreviewLink(string previewLink)
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            EBook eBook = new EBook("E-Book Title", BookGenre.FANTASY, author);

            // Act
            eBook.PreviewLink = previewLink;

            // Assert
            Assert.AreEqual(previewLink, eBook.PreviewLink);
        }

        [TestMethod]
        [DataRow("invalid-url")]
        [DataRow("ftp://invalidlink.com")]
        [DataRow("http://")]
        [DataRow("www.example.com")]
        [DataRow("://invalidlink.com")]
        public void ShouldThrowArgumentException_WhenSettingInvalidPreviewLink(string previewLink)
        {
            // Arrange
            Author author = new Author("Author", "LastName");
            EBook eBook = new EBook("E-Book Title", BookGenre.FANTASY, author);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => eBook.PreviewLink = previewLink);
        }
    }
}
