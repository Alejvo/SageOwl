using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Forms.Create;
using SageOwl.UI.ViewModels.Forms.Update;

namespace SageOwl.UI.Controllers;

public class FormController : Controller
{
    private readonly IFormService _formService;

    public FormController(IFormService formService)
    {
        _formService = formService;
    }

    // GET Methods
    [Route("form/{formId}")]
    public async Task<IActionResult> Index(Guid formId)
    {
        ViewBag.FormId = formId;
        ViewData["HeaderTitle"] = "Form";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");

        var form = await _formService.GetFormById(formId);
        return View(form);
    }

    // POST Methods
    [HttpPost]
    public async Task<IActionResult> Create(CreateFormViewModel createForm)
    {
        if (!ModelState.IsValid)
        {
            return View(createForm);
        }

        await _formService.CreateForm(createForm);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateFormViewModel updateForm)
    {
        if (!ModelState.IsValid)
        {
            return View(updateForm);
        }

        await _formService.UpdateForm(updateForm);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid formId)
    {
        if (formId == Guid.Empty)
            return View();

        await _formService.DeleteForm(formId);
        return View();
    }
}
