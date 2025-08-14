using Application.Abstractions;
using Domain.Announcements;
using Shared;

namespace Application.Announcements.Create;

internal sealed class CreateAnnouncementCommandHandler : ICommandHandler<CreateAnnouncementCommand>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public CreateAnnouncementCommandHandler(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<Result> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var newAnnouncement = Announcement.Create(request.Title,request.Content,request.AuthorId,request.Teamid);
        return await _announcementRepository.CreateAnnouncement(newAnnouncement)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
