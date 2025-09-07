using Application.Abstractions;
using Domain.Announcements;
using Domain.Teams;
using Microsoft.AspNetCore.Http;
using Shared;
using System.Security.Claims;

namespace Application.Announcements.Create;

internal sealed class CreateAnnouncementCommandHandler : ICommandHandler<CreateAnnouncementCommand>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateAnnouncementCommandHandler(
        IAnnouncementRepository announcementRepository,
        ITeamRepository teamRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _announcementRepository = announcementRepository;
        _teamRepository = teamRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User
                   .FindFirst("sub")?.Value
                   ?? _httpContextAccessor.HttpContext?.User
                   .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
            return Result.Failure(Error.Unauthorized);

        var role = await _teamRepository.GetUserRoleInTeam(userId, request.TeamId);

        if (role != "Admin")
            return Result.Failure(Error.Forbidden);

        var newAnnouncement = Announcement.Create(
            request.Title,
            request.Content,
            request.AuthorId,
            request.TeamId
        );

        var created = await _announcementRepository.CreateAnnouncement(newAnnouncement);

        return created
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}

