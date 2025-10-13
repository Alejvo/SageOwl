using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;
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

    public async Task<IActionResult> GetQualification(string period)
    {
        var qualifications = await _qualificationService.GetQualificationByUserId(_currentUser.Id);
        var qualification = qualifications.FirstOrDefault(x => x.Period == period);

        if (qualification is null)
            return NotFound();

        QualificationViewModel qualificationVM = new QualificationViewModel
        {
            Descriptions = qualification.UserQualifications.Select(x => x.Description).ToList(),
            Period = qualification.Period,
            TotalGrades = qualification.TotalGrades,
            PeriodList = qualifications.Select(x => x.Period).ToList(),
            UserQualifications = qualification.UserQualifications
                .GroupBy(uq => new { uq.UserId, uq.Name })
                .Select(g => new UserQualificationViewModel
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Grades = g.OrderBy(x => x.Position).Select(x => x.Grade).ToList(),
                    Positions = g.OrderBy(x => x.Position).Select(x => x.Position).ToList()
                }).ToList()
        };

        return PartialView("~/Views/Shared/PartialViews/_QualificationTable.cshtml", qualificationVM);
    }

}
