using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRManagementSystem.Data;
using HRManagementSystem.Models;

namespace HRManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext context) { _context = context; }

        // Create Employee - GET
        public IActionResult Create(int companyId)
        {
            if (companyId == 0)
                return RedirectToAction("Index", "Companies");

            var company = _context.Companies.Find(companyId);
            if (company == null)
                return RedirectToAction("Index", "Companies");

            ViewBag.CompanyId = companyId;
            ViewBag.CompanyName = company.Name;
            return View();
        }

        // Create Employee - POST
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            // Remove Company navigation property from validation
            ModelState.Remove("Company");

            if (employee.CompanyId == 0)
            {
                ViewBag.CompanyId = employee.CompanyId;
                ModelState.AddModelError("", "Company not found. Please go back and try again.");
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.CompanyId = employee.CompanyId;
            return View(employee);
        }

        // Delete Employee - POST
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Companies");
        }
    }
}