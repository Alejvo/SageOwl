using System.Text.Json.Serialization;

namespace Application.Forms.Common.Response;

public record FormResponse(
    Guid Id,
    string Title,
    Guid TeamId,
    DateTime Deadline,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    List<FormQuestionResponse>? Questions
    );
