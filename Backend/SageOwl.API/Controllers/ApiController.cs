using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared;

namespace SageOwl.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{

    private static readonly Dictionary<ErrorType, int> ErrorPriority = new()
    {
        {ErrorType.Forbidden,1 },
        {ErrorType.Unauthorized,2 },
        {ErrorType.NotFound,3 },
        {ErrorType.Conflict,4 },
        {ErrorType.Validation,5 },
        {ErrorType.Failure,6 }
    };

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors is null || errors.Count == 0)
        {
            return Problem(
                statusCode:StatusCodes.Status500InternalServerError,
                title:"Unexpected Error",
                detail: "An unexpected error occurred"
                );
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return CreateValidationProblem(errors);
        }

        var dominantError = errors
            .OrderBy(e => ErrorPriority.GetValueOrDefault(e.Type, int.MaxValue))
            .First();

        var dominantType = dominantError.Type;

        var filteredErrors = errors
            .Where(e => e.Type == dominantType)
            .ToList();

        var statusCode = MapStatusCode(dominantType);

        return CreateProblem(
            statusCode: statusCode,
            title: dominantError.Code,
            detail: dominantError.Description,
            errors: filteredErrors
            );
    }

    private IActionResult CreateProblem(
        int statusCode,
        string title,
        string detail,
        List<Error>? errors = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = $"errors/{title}"
        };

        if (errors is not null && errors.Count > 0)
        {
            problemDetails.Extensions["errors"] = errors.Select(e => new
            {
                e.Code,
                e.Description
            });
        }

        return StatusCode(statusCode, problemDetails);
    }

    private IActionResult CreateValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code ?? "ValidationError", 
                error.Description
                );
        }
        return ValidationProblem(modelStateDictionary);
    }

    private static int MapStatusCode(ErrorType type)
    {
        return type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
