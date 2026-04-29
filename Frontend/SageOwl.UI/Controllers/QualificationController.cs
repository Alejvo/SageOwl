using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
using SageOwl.UI.Models.Qualifications;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Qualifications;

namespace SageOwl.UI.Controllers;

public class QualificationController : Controller
{
    private readonly IQualificationService _qualificationService;
    private readonly CurrentUser _currentUser;

    public QualificationController(IQualificationService qualificationService, CurrentUser currentUser) 
    { 
        _qualificationService = qualificationService;
        _currentUser = currentUser;
    }

    // GET Methods
    public async Task<IActionResult> GetQualification(string period)
    {
        var qualifications = await _qualificationService.GetQualificationByUserId(_currentUser.Id);
        var qualification = qualifications.FirstOrDefault(x => x.Period == period);

        if (qualification is null)
            return NotFound();

        QualificationViewModel qualificationVM = new QualificationViewModel
        {
            QualificationId = qualification.Id,
            Descriptions = qualification.UserQualifications.Select(x => x.Description).ToList(),
            Period = qualification.Period,
            TotalGrades = qualification.TotalGrades,
            UserQualifications = qualification.UserQualifications
                .GroupBy(uq => new { uq.UserId, uq.Name })
                .Select(g => new UserQualificationViewModel
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Grades = g.Select(x => x.Grade).ToList()
                }).ToList()
        };

        return PartialView("~/Views/Shared/PartialViews/_QualificationTable.cshtml", qualificationVM);
    }

    //POST Methods
    public async Task<IActionResult> SaveQualifications(SaveQualification qualification)
    {
        if (ModelState.IsValid)
        {
            await _qualificationService.SaveQualifications(qualification);
        }

        return View(qualification);
    }

    public async Task<IActionResult> DeleteQualification(Guid qualificationId)
    {
        if (ModelState.IsValid)
        {
            await _qualificationService.DeleteQualification(qualificationId);
        }

        return View();
    }
}
