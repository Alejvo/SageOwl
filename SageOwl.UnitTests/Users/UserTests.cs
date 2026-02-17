using Domain.Users;
using FluentAssertions;

namespace SageOwl.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Should_Create_Valid_User()
    {
        // Arrange
        var id = Guid.NewGuid();
        var birthday = new DateTime(2000,1,1);

        // Act
        var user = User.Create(
            id,
            "John",
            "Smith",
            "john@test.com",
            "123456",
            "john123",
            birthday);

        // Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_When_Email_Is_Invalid()
    {
        // Arrange

        // Act
        Action act = () => User.Create(
            Guid.NewGuid(),
            "John",
            "Doe",
            "bad-email",
            "Password123!",
            "john",
            DateTime.Now);

        // Assert
        act.Should().Throw<Exception>();
    }
}
