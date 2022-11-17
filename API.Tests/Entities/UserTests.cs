using API.Entities;
using API.Entities.Enums;
using API.Entities.Utils;
using Xunit;

namespace API.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void User_Validate_Username_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrtbagrtbagrtbagrtbagrtbagrtbagrtbagrtbagrtbagrtbagrt",
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 "lucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O usuario deve ter ate 50 caracteres", result.Message);
        }        

        [Fact]
        public void User_Validate_Email_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 "lucasghanklucasghanklucasghanklucasghanklucasghanklucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O email deve ter ate 50 caracteres", result.Message);
        }

        [Fact]
        public void User_Validate_Username_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 string.Empty,
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 "lucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O usuario nao pode estar vazio", result.Message);
        }      
        [Fact]
        public void User_Validate_Password_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 string.Empty,
                 "lucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("A senha nao pode estar vazia", result.Message);
        }
        [Fact]
        public void User_Validate_Email_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 string.Empty,
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O email nao pode estar vazio", result.Message);
        }
        [Fact]
        public void User_Validate_Username_Null()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 null,
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 "lucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O usuario nao pode estar vazio", result.Message);
        }
        [Fact]
        public void User_Validate_Password_Null()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 null,
                 "lucasghank@hotmail.com",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("A senha nao pode estar vazia", result.Message);
        }
        [Fact]
        public void User_Validate_Email_Null()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 null,
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O email nao pode estar vazio", result.Message);
        }      
        [Fact]
        public void User_Validate_Email_IsValid()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new User(
                 "bagrt",
                 BCrypt.Net.BCrypt.HashPassword("123"),
                 "lucasghank",
                 Role.Admin,
                 status: Status.Active));

            //Assert
            Assert.Equal("O email deve ser valido", result.Message);
        }
    }
}
