using Application.Abstractions;

namespace Application.Qualifications.Commands.Delete;

public record DeleteQualificationCommand(
    Guid Id
    ):ICommand;
