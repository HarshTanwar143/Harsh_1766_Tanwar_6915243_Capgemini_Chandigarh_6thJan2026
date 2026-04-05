using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Services;
using SmartHospital.MVC.ViewModels;

namespace SmartHospital.MVC.Controllers;

public class AccountController : Controller
{
    private readonly ApiService _api;

    public AccountController(ApiService api) => _api = api;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var (success, token, role, userId, name, error) = await _api.LoginAsync(vm.Email, vm.Password);

        if (!success)
        {
            ModelState.AddModelError("", error ?? "Login failed.");
            return View(vm);
        }

        HttpContext.Session.SetString("JwtToken", token!);
        HttpContext.Session.SetString("UserRole", role!);
        HttpContext.Session.SetString("UserName", name!);
        HttpContext.Session.SetInt32("UserId", userId);

        return role switch
        {
            "Admin" => RedirectToAction("Index", "Admin"),
            "Doctor" => RedirectToAction("Index", "Doctor"),
            _ => RedirectToAction("Index", "Patient")
        };
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var (success, error) = await _api.RegisterAsync(vm.FullName, vm.Email, vm.Password);
        if (!success)
        {
            ModelState.AddModelError("", error ?? "Registration failed.");
            return View(vm);
        }

        TempData["Success"] = "Registered successfully. Please login.";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied() => View();
}
