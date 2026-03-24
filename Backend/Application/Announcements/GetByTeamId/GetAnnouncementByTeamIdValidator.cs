using FluentValidation;

namespace Application.Announcements.GetByTeamId;

public class GetAnnouncementByTeamIdValidator : AbstractValidator<GetAnnouncementByTeamIdQuery>
{
    public GetAnnouncementByTeamIdValidator()
    {
        RuleFor(a => a.TeamId).NotNull();
    }
}
