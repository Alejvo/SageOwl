using FluentValidation;

namespace Application.Announcements.Create;

public class CreateAnnouncementValidator : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementValidator()
    {
        RuleFor(a => a.Title).NotEmpty();

        RuleFor(a => a.Content).NotEmpty();

        RuleFor(a => a.AuthorId).NotNull();

        RuleFor(a => a.TeamId).NotNull();
    }
}
