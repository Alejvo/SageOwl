using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SageOwl.UI.Services.Interfaces;
using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;

    public AccountController(IUserService userService, IAccountService accountService)
    {
        _userService = userService;
        _accountService = accountService;
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
            await _accountService.Login(login);

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
}
