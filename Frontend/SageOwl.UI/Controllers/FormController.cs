using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models.Users;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.Forms.Update;

namespace SageOwl.UI.Controllers;

[Route("[controller]")]
[AuthorizeToken]
public class FormController : Controller
{
    private readonly IFormService _formService;
    private readonly ITeamService _teamService;
    private readonly CurrentUser _currentUser;

    public FormController(IFormService formService, ITeamService teamService, CurrentUser currentUser)
    {
        _formService = formService;
        _teamService = teamService;
        _currentUser = currentUser;
    }

    // GET Methods
    [Route("id/{formId}")]
    [HttpGet]
    public async Task<IActionResult> Index(Guid formId)
    {
        ViewBag.FormId = formId;
        ViewData["HeaderTitle"] = "Form";

        var form = await _formService.GetFormById(formId);
        return View(form);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var teamNames = await _teamService.GetNamesByAdminId(_currentUser.Id!.Value);

        var teamVM = new CreateFormViewModel
        {
            TeamNames = teamNames
        };

        return View(teamVM);
    }

    [HttpGet]
    [Route("update")]
    public IActionResult Update()
    {
        return View();
    }

    // POST Methods

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateFormViewModel createForm)
    {
        if (!ModelState.IsValid)
            return View(createForm);

        await _formService.CreateForm(createForm.NewForm);

        return RedirectToAction("Teams", "Workspace");
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateFormViewModel updateForm)
    {
        if (!ModelState.IsValid)
        {
            return View(updateForm);
        }

        await _formService.UpdateForm(updateForm);
        return View();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(Guid formId)
    {
        if (formId == Guid.Empty)
            return View();

        await _formService.DeleteForm(formId);
        return View();
    }
}
