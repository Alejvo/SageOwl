using Application.Interfaces;
using Application.Teams.Common;
using Domain.Users;
using FluentAssertions;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SageOwl.IntegrationTests.Teams;

public class GetTeamTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory _factory;


    public GetTeamTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task Should_Get_Teams_By_User_Id()
    {
        await Utilities.AuthenticateAsync(_client, _factory.Services);

        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = await dbContext.Users
            .FirstAsync(u => u.Email == Email.Create("user@test.com"));

        var userId = user.Id;

        var response = await _client.GetAsync($"/api/team/userId/{userId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var teams = JsonSerializer.Deserialize<List<TeamResponse>>(content,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        teams.Should().NotBeNull();
        teams!.Count.Should().Be(2);
        teams.Should().Contain(t => t.Name == "Team A");
        teams.Should().Contain(t => t.Name == "Team B");
        teams.Should().OnlyHaveUniqueItems(t => t.TeamId);

    }
}
