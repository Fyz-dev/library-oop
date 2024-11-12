using Library.Entities;

namespace UnitTests
{
    [TestClass]
    public class PublisherTests
    {
        [TestMethod]
        [DataRow("Penguin Books", "London, UK")]
        [DataRow("Oxford University Press", "Oxford, UK")]
        [DataRow("HarperCollins", "New York, USA")]
        public void Constructor_ShouldInitializePublisher_WithValidNameAndAddress(
            string name, string address)
        {
            // Arrange
            Publisher publisher = new Publisher(name, address);

            // Assert
            Assert.AreEqual(name, publisher.Name);
            Assert.AreEqual(address, publisher.Address);
        }

        [TestMethod]
        [DataRow(" ", "London, UK")]
        [DataRow("Penguin Books", "")]
        [DataRow(null, "London, UK")]
        [DataRow("Penguin Books", null)]
        [DataRow("Penguin@Books", "London, UK")] 
        [DataRow("Penguin Books", "123 Main St, NY")] 
        public void Constructor_ShouldThrowArgumentException_WithInvalidNameAndAddress(
             string name, string address)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Publisher(name, address));
        }
    }
}
