using Application.Users.Commands.Create;
using Domain.Users;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace SageOwl.IntegrationTests.Users;

public class CreateUserTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateUserTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_User_And_Persist_In_Database()
    {
        var request = new CreateUserCommand(
            "John",
            "Doe",
            "john@example.com",
            "Password123!",
            "johndoe",
            DateTime.Now.AddYears(-20));

        var response = await _client.PostAsJsonAsync("/api/users", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response.Content.Headers.ContentLength.Should().Be(0);
    }

}
