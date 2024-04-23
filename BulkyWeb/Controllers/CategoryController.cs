using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> CategoryList = _db.Categories.ToList();
            return View(CategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Data Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Data Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(c=>c.Id == id);
            if (category != null) {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                TempData["success"] = "Data Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();
            
            
        }
    }
}
