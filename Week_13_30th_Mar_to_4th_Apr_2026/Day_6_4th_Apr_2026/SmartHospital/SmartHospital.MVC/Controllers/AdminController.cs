using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Services;
using SmartHospital.MVC.ViewModels;

namespace SmartHospital.MVC.Controllers;

public class AdminController : Controller
{
    private readonly ApiService _api;

    public AdminController(ApiService api) => _api = api;

    private IActionResult RequireAdmin()
    {
        var role = HttpContext.Session.GetString("UserRole");
        if (role != "Admin") return RedirectToAction("Login", "Account");
        return null!;
    }

    public async Task<IActionResult> Index()
    {
        var check = RequireAdmin(); if (check != null) return check;

        ViewBag.Departments = await _api.GetDepartmentsAsync();
        ViewBag.Doctors = await _api.GetDoctorsAsync();
        ViewBag.Patients = await _api.GetPatientsAsync();
        ViewBag.Appointments = await _api.GetAllAppointmentsAsync();
        ViewBag.Bills = await _api.GetAllBillsAsync();
        return View();
    }

    // ---- DEPARTMENTS ----
    public async Task<IActionResult> Departments()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var depts = await _api.GetDepartmentsAsync();
        return View(depts);
    }

    [HttpGet]
    public IActionResult CreateDepartment() { RequireAdmin(); return View(); }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(DepartmentViewModel vm)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.CreateDepartmentAsync(vm.DepartmentName, vm.Description);
        TempData["Success"] = "Department created.";
        return RedirectToAction("Departments");
    }

    [HttpGet]
    public async Task<IActionResult> EditDepartment(int id)
    {
        var check = RequireAdmin(); if (check != null) return check;
        var dept = await _api.GetDepartmentAsync(id);
        if (dept == null) return NotFound();
        return View(dept);
    }

    [HttpPost]
    public async Task<IActionResult> EditDepartment(DepartmentViewModel vm)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.UpdateDepartmentAsync(vm.DepartmentId, vm.DepartmentName, vm.Description);
        TempData["Success"] = "Department updated.";
        return RedirectToAction("Departments");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.DeleteDepartmentAsync(id);
        TempData["Success"] = "Department deleted.";
        return RedirectToAction("Departments");
    }

    // ---- DOCTORS ----
    public async Task<IActionResult> Doctors()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var doctors = await _api.GetDoctorsAsync();
        return View(doctors);
    }

    [HttpGet]
    public async Task<IActionResult> CreateDoctor()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var vm = new CreateDoctorViewModel { Departments = await _api.GetDepartmentsAsync() };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(CreateDoctorViewModel vm)
    {
        var check = RequireAdmin(); if (check != null) return check;
        if (!ModelState.IsValid)
        {
            vm.Departments = await _api.GetDepartmentsAsync();
            return View(vm);
        }
        var ok = await _api.CreateDoctorAsync(vm);
        if (!ok) { TempData["Error"] = "Failed to create doctor. Email may be taken."; vm.Departments = await _api.GetDepartmentsAsync(); return View(vm); }
        TempData["Success"] = "Doctor created successfully.";
        return RedirectToAction("Doctors");
    }

    [HttpGet]
    public async Task<IActionResult> EditDoctor(int id)
    {
        var check = RequireAdmin(); if (check != null) return check;
        var doctor = await _api.GetDoctorAsync(id);
        if (doctor == null) return NotFound();
        ViewBag.Departments = await _api.GetDepartmentsAsync();
        return View(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> EditDoctor(DoctorViewModel vm)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.UpdateDoctorAsync(vm.DoctorId, new UpdateDoctorPayload
        {
            DepartmentId = vm.DepartmentId,
            Specialization = vm.Specialization,
            ExperienceYears = vm.ExperienceYears,
            Availability = vm.Availability
        });
        TempData["Success"] = "Doctor updated.";
        return RedirectToAction("Doctors");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.DeleteDoctorAsync(id);
        TempData["Success"] = "Doctor deleted.";
        return RedirectToAction("Doctors");
    }

    // ---- PATIENTS ----
    public async Task<IActionResult> Patients()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var patients = await _api.GetPatientsAsync();
        return View(patients);
    }

    // ---- APPOINTMENTS ----
    public async Task<IActionResult> Appointments()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var appts = await _api.GetAllAppointmentsAsync();
        return View(appts);
    }

    // ---- BILLS ----
    public async Task<IActionResult> Bills()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var bills = await _api.GetAllBillsAsync();
        return View(bills);
    }

    [HttpPost]
    public async Task<IActionResult> MarkPaid(int id)
    {
        var check = RequireAdmin(); if (check != null) return check;
        await _api.UpdatePaymentStatusAsync(id, "Paid");
        TempData["Success"] = "Bill marked as Paid.";
        return RedirectToAction("Bills");
    }

    // ---- PRESCRIPTIONS ----
    public async Task<IActionResult> Prescriptions()
    {
        var check = RequireAdmin(); if (check != null) return check;
        var list = await _api.GetAllPrescriptionsAsync();
        return View(list);
    }
}
