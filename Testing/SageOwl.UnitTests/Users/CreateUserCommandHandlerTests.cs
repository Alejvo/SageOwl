using Application.Users.Commands.Create;
using Application.Users.Events;
using Domain.Users;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Shared;

namespace SageOwl.UnitTests.Users;

public class CreateUserCommandHandlerTests
{
    private readonly IUserRepository _userRepositoryMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly IMediator _mediatorMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _mediatorMock = Substitute.For<IMediator>();

        _handler = new CreateUserCommandHandler(
            _userRepositoryMock,
            _passwordHasherMock,
            _mediatorMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_UserIsCreated()
    {
        // Arrange
        var command = new CreateUserCommand(
            "John", 
            "Doe", 
            "john@example.com", 
            "Password123!", 
            "johndoe", 
            DateTime.Now.AddYears(-20));

        _userRepositoryMock.EmailExists(command.Email).Returns(false);
        _userRepositoryMock.UsernameExists(command.Username).Returns(false);
        _passwordHasherMock.Hash(command.Password).Returns("hashed_password");
        _userRepositoryMock.CreateUser(Arg.Any<User>()).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        await _userRepositoryMock.Received(1).CreateUser(Arg.Is<User>(u => u.Email.ToString() == command.Email));

        await _mediatorMock.Received(1).Publish(
            Arg.Any<UserCreatedNotification>(),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_EmailAlreadyExists()
    {
        // Arrange
        var command = new CreateUserCommand(
            "John", 
            "Doe", 
            "duplicate@example.com", 
            "Password123!", 
            "johndoe", 
            DateTime.Now.AddYears(-20));

        _userRepositoryMock.EmailExists(command.Email).Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(UserErrors.EmailAlreadyExists(command.Email));

        _passwordHasherMock.DidNotReceive().Hash(Arg.Any<string>());
        await _userRepositoryMock.DidNotReceive().CreateUser(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_DatabaseFails()
    {
        // Arrange
        var command = new CreateUserCommand(
            "John", 
            "Doe", 
            "john@example.com", 
            "123456", 
            "johndoe", 
            DateTime.Now.AddYears(-20));

        _userRepositoryMock.EmailExists(command.Email).Returns(false);
        _userRepositoryMock.UsernameExists(command.Username).Returns(false);
        _passwordHasherMock.Hash(Arg.Any<string>()).Returns("HASHED_PASSWORD_SECURE");

        _userRepositoryMock.CreateUser(Arg.Any<User>()).Returns(false);

        // Act

        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(Error.DBFailure);
    }
}
