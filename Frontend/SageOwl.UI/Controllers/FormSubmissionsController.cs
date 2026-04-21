using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.FormSubmissions;

namespace SageOwl.UI.Controllers;

public class FormSubmissionsController : Controller
{
    private readonly IFormSubmissionService _formSubmissionService;

    public FormSubmissionsController(IFormSubmissionService formSubmissionService)
    {
        _formSubmissionService = formSubmissionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFormSubmissionViewModel createFormSubmission)
    {
        if (!ModelState.IsValid)
        {
            return View(createFormSubmission);
        }

        await _formSubmissionService.CreateFormSubmission(createFormSubmission);

        return View();
    }
}
