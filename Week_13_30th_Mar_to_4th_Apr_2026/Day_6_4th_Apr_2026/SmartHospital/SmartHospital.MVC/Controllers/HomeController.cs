using Microsoft.AspNetCore.Mvc;

namespace SmartHospital.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var role = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Account");
        return role switch
        {
            "Admin"  => RedirectToAction("Index", "Admin"),
            "Doctor" => RedirectToAction("Index", "Doctor"),
            _        => RedirectToAction("Index", "Patient")
        };
    }
}
