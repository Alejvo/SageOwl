using Microsoft.AspNetCore.Mvc;

namespace SageOwl.UI.Controllers;

public class FormController : Controller
{
    [Route("form/{formId}")]
    public IActionResult Index(string formId)
    {
        ViewBag.FormId = formId;
        return View();
    }

    [Route("form/create")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("form/{formId}/update")]
    public IActionResult Update(string formId)
    {
        ViewBag.FormId = formId;
        return View();
    }

    [Route("form/{formId}/results")]
    public IActionResult Results(string formId)
    {
        ViewBag.FormId = formId;
        return View();
    }
}
