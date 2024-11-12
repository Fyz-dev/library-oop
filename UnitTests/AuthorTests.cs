using Library.Entities;

namespace UnitTests
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        [DataRow("J.K.", "Rowling", "J.K. Rowling")] 
        [DataRow("John", "Doe", "John Doe")]
        [DataRow("Anna", "Smith", "Anna Smith")]
        [DataRow("J", "D", "J D")]
        public void Constructor_ShouldInitializeAuthor_WithValidFirstNameAndLastName(
            string firstName, string lastName, string expectedFullName)
        {
            // Arrange
            Author author = new Author(firstName, lastName);

            // Assert
            Assert.AreEqual(firstName, author.FirstName);
            Assert.AreEqual(lastName, author.LastName);
            Assert.AreEqual(expectedFullName, author.FullName);
            Assert.IsNull(author.Biography);
        }

        [TestMethod]
        [DataRow(" ", "Rowling")]
        [DataRow("J.K.", "")]
        [DataRow(null, "Rowling")]
        [DataRow("J.K.", null)]
        [DataRow("J@K", "Rowling")]
        [DataRow("J.K.", "R0wling")]
        public void Constructor_ShouldThrowArgumentException_WithInvalidFirstNameAndLastName(
            string firstName,
            string lastName
        )
        {
            Assert.ThrowsException<ArgumentException>(() => new Author(firstName, lastName));
        }
    }
}
