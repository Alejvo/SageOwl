using System.Text.Json.Serialization;

namespace Application.Forms.Common.Response;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(OpenQuestionResponse), "open")]
[JsonDerivedType(typeof(ClosedQuestionResponse), "closed")]
public abstract record QuestionResponse(
    Guid Id,
    string Text
    );
