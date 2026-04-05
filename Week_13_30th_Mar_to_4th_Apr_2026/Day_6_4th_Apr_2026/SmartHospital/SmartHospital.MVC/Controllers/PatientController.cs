using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Services;
using SmartHospital.MVC.ViewModels;

namespace SmartHospital.MVC.Controllers;

public class PatientController : Controller
{
    private readonly ApiService _api;
    public PatientController(ApiService api) => _api = api;

    private IActionResult? RequirePatient()
    {
        var role = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Account");
        return null;
    }

    public async Task<IActionResult> Index()
    {
        var check = RequirePatient(); if (check != null) return check;
        var appts = await _api.GetMyAppointmentsAsync();
        return View(appts);
    }

    public async Task<IActionResult> Appointments()
    {
        var check = RequirePatient(); if (check != null) return check;
        var appts = await _api.GetMyAppointmentsAsync();
        return View(appts);
    }

    [HttpGet]
    public async Task<IActionResult> BookAppointment()
    {
        var check = RequirePatient(); if (check != null) return check;
        var vm = new BookAppointmentViewModel
        {
            Departments = await _api.GetDepartmentsAsync(),
            Doctors = await _api.GetDoctorsAsync()
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment(BookAppointmentViewModel vm)
    {
        var check = RequirePatient(); if (check != null) return check;

        if (vm.DoctorId == 0 || vm.AppointmentDate == default)
        {
            ModelState.AddModelError("", "Please select a doctor and date.");
            vm.Departments = await _api.GetDepartmentsAsync();
            vm.Doctors = await _api.GetDoctorsAsync();
            return View(vm);
        }

        var ok = await _api.BookAppointmentAsync(vm.DoctorId, vm.AppointmentDate);
        if (!ok)
        {
            TempData["Error"] = "Failed to book appointment.";
            vm.Departments = await _api.GetDepartmentsAsync();
            vm.Doctors = await _api.GetDoctorsAsync();
            return View(vm);
        }

        TempData["Success"] = "Appointment booked successfully!";
        return RedirectToAction("Appointments");
    }

    [HttpPost]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var check = RequirePatient(); if (check != null) return check;
        await _api.CancelAppointmentAsync(id);
        TempData["Success"] = "Appointment cancelled.";
        return RedirectToAction("Appointments");
    }

    public async Task<IActionResult> ViewPrescription(int appointmentId)
    {
        var check = RequirePatient(); if (check != null) return check;
        var prescription = await _api.GetPrescriptionByAppointmentAsync(appointmentId);
        if (prescription == null) { TempData["Error"] = "No prescription found."; return RedirectToAction("Appointments"); }
        return View(prescription);
    }

    public async Task<IActionResult> Bills()
    {
        var check = RequirePatient(); if (check != null) return check;
        var bills = await _api.GetMyBillsAsync();
        return View(bills);
    }

    public async Task<IActionResult> ViewBill(int appointmentId)
    {
        var check = RequirePatient(); if (check != null) return check;
        var bill = await _api.GetBillByAppointmentAsync(appointmentId);
        if (bill == null) { TempData["Error"] = "Bill not generated yet."; return RedirectToAction("Appointments"); }
        return View(bill);
    }
}
