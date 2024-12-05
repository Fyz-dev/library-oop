using Library.Entities;
using Library.Enums;

namespace UnitTests
{
    [TestClass]
    public class VisitorTests
    {
        [TestMethod]
        [DataRow("John", 0)]
        [DataRow("Jane", 0)]
        public void Constructor_ShouldInitializeVisitor_WithValidParameters(
            string name,
            int expectedBookCount
        )
        {
            // Arrange & Act
            Visitor visitor = new Visitor(name);

            // Assert
            Assert.AreEqual(name, visitor.Name);
            Assert.AreEqual(expectedBookCount, visitor.Count());
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("A very long name that exceeds twenty five characters")]
        [DataRow("John1")]
        public void Name_ShouldThrowArgumentException_WithInvalidName(string name)
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Visitor(name));
        }

        [TestMethod]
        public void TakeBook_ShouldAddBook_ToTakenBooks()
        {
            // Arrange
            var visitor = new Visitor("John Doe");
            var book = new EBook(
                "Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author>([new Author("firstname", "lastname", "bio")]),
                "1234567890123",
                "http://previewlink.com"
            );

            // Act
            visitor.TakeBook(book);

            // Assert
            Assert.AreEqual(1, visitor.Count());
        }

        [TestMethod]
        public void TakeBook_ShouldThrowInvalidOperationException_IfBookAlreadyTaken()
        {
            // Arrange
            var visitor = new Visitor("John Doe");
            var book = new EBook(
                "Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author>([new Author("firstname", "lastname", "bio")]),
                "1234567890123",
                "http://previewlink.com"
            );
            visitor.TakeBook(book);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => visitor.TakeBook(book));
        }

        [TestMethod]
        public void ReturnBook_ShouldRemoveBook_FromTakenBooks()
        {
            // Arrange
            var visitor = new Visitor("John Doe");
            var book = new EBook(
                "Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author>([new Author("firstname", "lastname", "bio")]),
                "1234567890123",
                "http://previewlink.com"
            );
            visitor.TakeBook(book);

            // Act
            visitor.ReturnBook(book);

            // Assert
            Assert.AreEqual(0, visitor.Count());
        }

        [TestMethod]
        public void ReturnBook_ShouldThrowInvalidOperationException_IfBookNotTaken()
        {
            // Arrange
            var visitor = new Visitor("John Doe");
            var book = new EBook(
                "Title",
                "Description",
                new List<BookGenre> { BookGenre.Fantasy },
                new List<Author>([new Author("firstname", "lastname", "bio")]),
                "1234567890123",
                "http://previewlink.com"
            );

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => visitor.ReturnBook(book));
        }
    }
}
