using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Models;

namespace SageOwl.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Plan> planList = new List<Plan>
            { 
                new Plan{
                    Title = "Free Plan", 
                    NumberForms=20,
                    NumberAccounts=20,
                    NumberTeams=20,
                    YearlyCost=0
                },
                new Plan{
                    Title = "Premium Plan",
                    NumberForms=50,
                    NumberAccounts=50,
                    NumberTeams=50,
                    YearlyCost=39.99M
                },
                new Plan{
                    Title = "Deluxe Plan",
                    NumberForms=60,
                    NumberAccounts=60,
                    NumberTeams=60,
                    YearlyCost=29.99M
                }
            };

            return View(planList);
        }

        public IActionResult News()
        {
            return View();
        }
    }
}
