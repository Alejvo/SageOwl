using Application.Abstractions;
using Application.Interfaces;
using Application.Forms.Common;
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
        if (team == null)
            return Result.Failure(TeamErrors.NotFound);

        foreach (var questionDto in request.Questions)
        {
            var question = QuestionFactory.Create(questionDto);
            form.AddQuestion(question);
        }

        await _formRepository.CreateForm(form);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
