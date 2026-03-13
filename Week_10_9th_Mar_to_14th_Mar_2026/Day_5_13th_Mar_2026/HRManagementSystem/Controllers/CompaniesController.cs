using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRManagementSystem.Data;
using HRManagementSystem.Models;

namespace HRManagementSystem.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;
        public CompaniesController(AppDbContext context) { _context = context; }

        // List all companies with employees
        public IActionResult Index()
        {
            var companies = _context.Companies.Include(c => c.Employees).ToList();
            return View(companies);
        }

        // Create - GET
        public IActionResult Create() => View();

        // Create - POST
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Add(company);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // Edit - GET
        public IActionResult Edit(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null) return NotFound();
            return View(company);
        }

        // Edit - POST
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Update(company);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // Delete - GET
        public IActionResult Delete(int id)
        {
            var company = _context.Companies.Include(c => c.Employees).FirstOrDefault(c => c.Id == id);
            if (company == null) return NotFound();
            return View(company);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var company = _context.Companies.Include(c => c.Employees).FirstOrDefault(c => c.Id == id);
            if (company != null)
            {
                _context.Employees.RemoveRange(company.Employees);
                _context.Companies.Remove(company);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}