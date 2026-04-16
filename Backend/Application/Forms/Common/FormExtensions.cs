using Application.Forms.Common.Response;
using Domain.Forms;

namespace Application.Forms.Common;

public static class FormExtensions
{
    public static FormResponse ToFormResponse(this Form form)
    {
        return new FormResponse(
            form.Id,
            form.Title,
            form.TeamId,
            form.Deadline,
            form.Questions?.Select(MapResponse).ToList() ?? []
        );
    }
    public static OptionResponse ToResponse(this Option option)
        => new(option.Value, option.IsCorrect);

    private static QuestionResponse MapResponse(Question question)
    {
        return question switch
        {
            OpenQuestion open =>
                new OpenQuestionResponse(open.Id, open.Text),

            ClosedQuestion closed =>
                new ClosedQuestionResponse(
                    closed.Id,
                    closed.Text,
                    [.. closed.Options.Select(o => o.ToResponse())]
                ),

            _ => throw new Exception("Unknown type")
        };
    }
}
