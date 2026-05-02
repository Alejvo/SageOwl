using Microsoft.AspNetCore.Mvc;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModels.UI;
using SageOwl.UI.ViewModels.Users;

namespace SageOwl.UI.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AccountController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            await _authService.Login(login);

            return RedirectToAction("Index", "Workspace");
        }
        return View(login);
    }
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid) 
        { 
            await _userService.Create(register);
            return RedirectToAction("Index","Home");
        }
        return View(register);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");

        return RedirectToAction("Index", "Home");
    }
}
