using Application.Forms.Common.Request;
using Domain.Forms;

namespace Application.Forms.Common;

public static class QuestionFactory
{
    public static Question Create(CreateQuestionRequest dto)
    {
        return dto.QuestionType switch
        {
            "closed" => ToClosedQuestion(dto),
            "open" => ToOpenQuestion(dto),
            _ => throw new NotSupportedException($"Unsupported type: {dto.QuestionType}")
        };
    }

    private static ClosedQuestion ToClosedQuestion(CreateQuestionRequest dto)
    {
        if (dto.Options == null || dto.Options.Count == 0)
            throw new ArgumentException("Closed question requires options");

        var options = dto.Options
            .Select(o => Option.Create(o.Value,o.IsCorrect));

       var question = new ClosedQuestion(dto.Text);

        foreach(var option in options)
        {
            question.AddOption(option);
        }

        return question;
    }

    private static OpenQuestion ToOpenQuestion(CreateQuestionRequest dto)
    {
        return new OpenQuestion(dto.Text);
    }
}
