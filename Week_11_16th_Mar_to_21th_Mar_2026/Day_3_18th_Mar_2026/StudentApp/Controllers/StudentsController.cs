using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;

namespace StudentApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Students/Index
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // POST: Add new student (from modal)
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                TempData["Success"] = "Student added successfully!";
            }
            return RedirectToAction("Index");
        }

        // GET: Get student data for Edit modal (returns JSON)
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return Json(student);
        }

        // POST: Edit existing student (from modal)
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                TempData["Success"] = "Student updated successfully!";
            }
            return RedirectToAction("Index");
        }

        // POST: Delete student
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                TempData["Success"] = "Student deleted successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}