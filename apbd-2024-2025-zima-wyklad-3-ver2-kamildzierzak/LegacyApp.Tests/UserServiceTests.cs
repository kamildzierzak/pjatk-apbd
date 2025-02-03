namespace LegacyApp.Tests

{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_Should_Return_False_When_Missing_FirstName()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser(null, "Bandy", "test@example.com", new DateTime(2000, 1, 1), 7);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Missing_LastName()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", null, "test@example.com", new DateTime(2000, 1, 1), 7);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Missing_At_Sign_And_Dot_In_Email()
        {
            // Arragne
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Bandy", "testexamplecom", new DateTime(2000, 1, 1), 7);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Is_Younger_Than_21()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Bandy", "test@example.com", DateTime.Today.AddDays(-1), 7);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddUser_Should_Throw_Exception_When_User_Does_Not_Exist()
        {
            // Arrange
            var service = new UserService();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => service.AddUser("Andy", "Bandy", "test@example.com", new DateTime(2000, 1, 1), 7));

            // Assert
            Assert.Equal("User with id 7 does not exist in database", exception.Message);
        }

        [Fact]
        public void AddUser_Should_Throw_Exception_When_User_With_No_Credit_Limit_Exists()
        {
            // Arrange
            var service = new UserService();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => service.AddUser("Andy", "Andrzejewicz", "test@example.com", new DateTime(2000, 1, 1), 6));

            // Assert
            Assert.Equal("Client Andrzejewicz does not exist", exception.Message);
        }

        [Fact]
        public void AddUser_Should_Return_True_When_Client_Is_Very_Important()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Malewski", "malewski@gmail.pl", new DateTime(2000, 1, 1), 2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddUser_Should_Return_True_When_Client_Is_Important()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Smith", "smith@gmail.pl", new DateTime(2000, 1, 1), 3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddUser_Should_Return_True_When_Client_Is_Normal()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Kwiatkowski", "kwiatkowski@wp.pl", new DateTime(2000, 1, 1), 5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddUser_Should_Return_False_When_Client_Is_Normal_With_No_Credit_Limit()
        {
            // Arrange
            var service = new UserService();

            // Act
            var result = service.AddUser("Andy", "Kowalski", "kowalski@wp.pl", new DateTime(2000, 1, 1), 1);

            // Assert
            Assert.False(result);
        }
    }
}