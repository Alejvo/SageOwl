using Application.Abstractions;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.Save;

internal sealed class SaveQualificationCommandHandler : ICommandHandler<SaveQualificationCommand>
{
    private readonly IQualificationRepository _qualificationRepository;

    public SaveQualificationCommandHandler(IQualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    public async Task<Result> Handle(SaveQualificationCommand request, CancellationToken cancellationToken)
    {

        var qualification = Qualification.Create(request.TeamId, request.MinimumGrade, request.MaximumGrade, request.PassingGrade, request.Period,request.TotalGrades);
        
        foreach (var uq in request.UserQualifications)
        {
            qualification.AddUserQualification(uq.UserId, uq.Grade, uq.Position, uq.HasValue, uq.Description);
        }
        
        return await _qualificationRepository.CreateQualifications(qualification)
                ? Result.Success()
                : Result.Failure(Error.DBFailure);
        
    }
}
