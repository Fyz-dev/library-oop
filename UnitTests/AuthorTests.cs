using Library.Entities;

namespace UnitTests
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        [DataRow("JK", "Rowling", "Famous author of Harry Potter", "JK Rowling")]
        [DataRow("John", "Doe", null, "John Doe")]
        [DataRow("Anna", "Smith", "Author of mystery novels", "Anna Smith")]
        [DataRow("J", "D", "", "J D")]
        public void Constructor_ShouldInitializeAuthor_WithValidParameters(
            string firstName,
            string lastName,
            string biography,
            string expectedFullName
        )
        {
            // Arrange & Act
            Author author = new Author(firstName, lastName, biography);

            // Assert
            Assert.AreEqual(firstName, author.FirstName);
            Assert.AreEqual(lastName, author.LastName);
            Assert.AreEqual(biography, author.Biography);
            Assert.AreEqual(expectedFullName, author.FullName);
        }

        [TestMethod]
        [DataRow("", "Rowling", "Biography")]
        [DataRow("J.K.", "", "Biography")]
        [DataRow(null, "Rowling", "Biography")]
        [DataRow("J.K.", null, "Biography")]
        [DataRow("J@K", "Rowling", "Biography")]
        [DataRow("J.K.", "R0wling", "Biography")]
        [DataRow("J.K.", "Rowling", null)]
        [DataRow("J.K.", "Rowling", "")]
        [DataRow(
            "J.K.",
            "Rowling",
            "This biography exceeds the maximum allowed length of 150 characters."
                + "This biography exceeds the maximum allowed length of 150 characters."
                + "This biography exceeds the maximum allowed length of 150 characters. "
        )]
        public void Constructor_ShouldThrowArgumentException_WithInvalidFirstNameOrLastName(
            string firstName,
            string lastName,
            string biography
        )
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Author(firstName, lastName, biography)
            );
        }
    }
}
