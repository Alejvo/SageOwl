using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

public class FormController : Controller
{
    [Route("form/{formId}")]
    public IActionResult Index(string formId)
    {
        ViewBag.FormId = formId;
        ViewData["HeaderTitle"] = "Form";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace");
        return View();
    }

    [Route("form/create")]
    public IActionResult Create()
    {
        ViewData["HeaderTitle"] = "Create Form";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace"); 
        return View();
    }

    [Route("form/{formId}/update")]
    public IActionResult Update(string formId)
    {
        ViewBag.FormId = formId;
        ViewData["HeaderTitle"] = "Update Form";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace"); 
        return View();
    }

    [Route("form/{formId}/results")]
    public IActionResult Results(string formId)
    {
        ViewBag.FormId = formId;
        ViewData["HeaderTitle"] = "Results";
        ViewData["HeaderUrl"] = Url.Action("Index", "Workspace"); 
        return View();
    }
}
