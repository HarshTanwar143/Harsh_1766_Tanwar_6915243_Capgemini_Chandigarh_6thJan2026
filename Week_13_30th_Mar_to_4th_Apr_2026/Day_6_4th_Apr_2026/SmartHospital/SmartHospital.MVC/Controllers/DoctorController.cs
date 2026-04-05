using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Services;
using SmartHospital.MVC.ViewModels;

namespace SmartHospital.MVC.Controllers;

public class DoctorController : Controller
{
    private readonly ApiService _api;
    public DoctorController(ApiService api) => _api = api;

    private IActionResult? RequireDoctor()
    {
        var role = HttpContext.Session.GetString("UserRole");
        if (role != "Doctor") return RedirectToAction("Login", "Account");
        return null;
    }

    public async Task<IActionResult> Index()
    {
        var check = RequireDoctor(); if (check != null) return check;
        var appts = await _api.GetMyAppointmentsAsync();
        return View(appts);
    }

    public async Task<IActionResult> Appointments()
    {
        var check = RequireDoctor(); if (check != null) return check;
        var appts = await _api.GetMyAppointmentsAsync();
        return View(appts);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteAppointment(int id)
    {
        var check = RequireDoctor(); if (check != null) return check;
        await _api.UpdateAppointmentStatusAsync(id, "Completed");
        TempData["Success"] = "Appointment marked as completed.";
        return RedirectToAction("Appointments");
    }

    // Prescription
    [HttpGet]
    public async Task<IActionResult> CreatePrescription(int appointmentId)
    {
        var check = RequireDoctor(); if (check != null) return check;
        var appt = await _api.GetAppointmentAsync(appointmentId);
        if (appt == null) return NotFound();

        var vm = new CreatePrescriptionViewModel
        {
            AppointmentId = appointmentId,
            PatientName = appt.PatientName
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(CreatePrescriptionViewModel vm)
    {
        var check = RequireDoctor(); if (check != null) return check;
        if (!ModelState.IsValid) return View(vm);

        var ok = await _api.CreatePrescriptionAsync(vm);
        if (!ok) { TempData["Error"] = "Failed to create prescription."; return View(vm); }

        TempData["Success"] = "Prescription created. You can approve it to generate a bill.";
        return RedirectToAction("Appointments");
    }

    [HttpGet]
    public async Task<IActionResult> ViewPrescription(int appointmentId)
    {
        var check = RequireDoctor(); if (check != null) return check;
        var prescription = await _api.GetPrescriptionByAppointmentAsync(appointmentId);
        if (prescription == null) return NotFound();
        return View(prescription);
    }

    [HttpGet]
    public async Task<IActionResult> ApprovePrescription(int id)
    {
        var check = RequireDoctor(); if (check != null) return check;
        var prescription = await _api.GetPrescriptionAsync(id);
        if (prescription == null) return NotFound();

        var vm = new ApprovePrescriptionViewModel
        {
            PrescriptionId = id,
            MedicineCharges = prescription.MedicineCharges
        };
        ViewBag.Prescription = prescription;
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> ApprovePrescription(ApprovePrescriptionViewModel vm)
    {
        var check = RequireDoctor(); if (check != null) return check;
        if (!ModelState.IsValid) return View(vm);

        var (success, total) = await _api.ApprovePrescriptionAsync(vm.PrescriptionId, vm.MedicineCharges);
        if (!success) { TempData["Error"] = "Approval failed."; return View(vm); }

        TempData["Success"] = $"Prescription approved! Bill generated. Total: ₹{total:F2}";
        return RedirectToAction("Appointments");
    }
}
