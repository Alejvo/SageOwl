using Application.Abstractions;
using Application.Interfaces;
using Domain.Forms;
using Domain.Teams;
using Shared;

namespace Application.Forms.Commands.Create;

internal sealed class CreateFormCommandHandler : ICommandHandler<CreateFormCommand>
{
    private readonly IFormRepository _formRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFormCommandHandler(
        IFormRepository formRepository,
        ITeamRepository teamRepository,
        IUnitOfWork unitOfWork
        )
    {
        _formRepository = formRepository;
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var form = Form.Create(request.TeamId, request.Title, request.Deadline);

        var team = await _teamRepository.GetTeamById(form.TeamId);

        var userIds = new List<Guid>();

        foreach (var member in team!.Members)
        {
            if (member.Role == TeamRole.Member)
            {
                userIds.Add(member.UserId);
            }
        }

        form.AddResults(userIds);

        foreach (var question in request.Questions)
        {
            var newQuestion = form.AddQuestion(question.Title, question.Description, form.Id,QuestionType.FromString(question.QuestionType));
            if (question.Options is not null)
            {
                foreach (var option in question.Options)
                {
                    newQuestion.AddOption(option.Value, option.IsCorrect);
                }
            }

        }

        await _formRepository.CreateForm(form);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
