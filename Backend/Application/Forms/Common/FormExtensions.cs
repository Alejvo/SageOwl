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
            form.Questions != null && form.Questions.Any()
                ?form.Questions.Select(fq => new FormQuestionResponse
                (
                    fq.Title,
                    fq.Description,
                    fq.QuestionType.ToString(),
                    fq.Options.Select(fo => new FormOptionResponse(
                    fo.Value,
                    fo.IsCorrect)).ToList()
                )).ToList()
                : null
        );
    }
}
