using Application.Users.Common;
using Domain.Announcements;
using Domain.Forms;
using Domain.Qualifications;
using System.Text.Json.Serialization;

namespace Application.Teams.Common;

public record TeamResponse(
    Guid TeamId,
    string Name,
    string Description,
    List<TeamMemberResponse> Members,
    List<FormDto> Forms,
    List<AnnouncementDto>? Announcements
);
