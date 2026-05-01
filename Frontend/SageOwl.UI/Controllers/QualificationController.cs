using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Attributes;
using SageOwl.UI.Models;
using SageOwl.UI.Models.Qualifications;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.Qualifications;

namespace SageOwl.UI.Controllers;

[Route("[controller]")]
[AuthorizeToken]
public class QualificationController : Controller
{
    private readonly IQualificationService _qualificationService;
    private readonly CurrentUser _currentUser;
    private readonly CurrentQualifications _currentQualifications;

    public QualificationController(
        IQualificationService qualificationService, 
        CurrentUser currentUser,
        CurrentQualifications currentQualifications) 
    { 
        _qualificationService = qualificationService;
        _currentUser = currentUser;
        _currentQualifications = currentQualifications;
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

    [HttpGet]
    [Route("QualificationPartial")]
    public IActionResult QualificationPartial(Guid id)
    {
        var qualification = _currentQualifications.Qualifications.FirstOrDefault(q => q.Id == id);

        if(qualification != null)
            _currentQualifications.CurrentId = qualification.Id;

        var qualificationVM = new QualificationViewModel
        {
            QualificationId = qualification.Id,
            Period = qualification.Period,
            TotalGrades = qualification.TotalGrades,
            Descriptions = qualification.UserQualifications.Select(x => x.Description).Distinct().ToList(),
            UserQualifications = qualification.UserQualifications
                .GroupBy(uq => new { uq.UserId, uq.Name })
                .Select(g => new UserQualificationViewModel
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Grades = g.Select(x => x.Grade).ToList(),
                }).ToList()
        };
        return PartialView(
            "~/Views/Shared/PartialViews/Qualifications/_QualificationTable.cshtml",
            qualificationVM
        );
    }

    //POST Methods
    [HttpPost("qualifications/save")]
    public async Task<IActionResult> SaveQualifications(SaveQualificationsViewModel qualification)
    {
        if (ModelState.IsValid)
        {
            var newQualification = new SaveQualification
            {
                MaximumGrade = qualification.MaximumGrade,
                MinimumGrade = qualification.MinimumGrade,
                PassingGrade = qualification.PassingGrade,
                TotalGrades = qualification.TotalGrades,
                Period = qualification.Period,
                TeamId = qualification.TeamId,
                UserQualifications = qualification.UserQualifications
                .SelectMany(uq => uq.Grades.Select((grade, gIndex) => new SaveUserQualification
                {
                    UserId = uq.UserId,
                    Grade = grade,
                    Description = qualification.Descriptions.Count > gIndex ? qualification.Descriptions[gIndex] : string.Empty
                })).ToList()
            };

            await _qualificationService.SaveQualifications(newQualification);
            return RedirectToAction("MainPage", "Team", new { teamId = newQualification.TeamId });
        }
        return View(qualification);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteQualification(Guid teamId)
    {
        var qualification = _currentQualifications.Qualifications
            .FirstOrDefault(x => x.Id == _currentQualifications.CurrentId);

        if(qualification != null)
            _currentQualifications.Qualifications.Remove(qualification);

        await _qualificationService.DeleteQualification(_currentQualifications.CurrentId);

        return RedirectToAction("MainPage", "Team", new { teamId = teamId });

    }
}
