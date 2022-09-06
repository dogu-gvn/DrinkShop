using FYD.DataAccess.Repository.IRepository;
using FYD.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinddYourDrink.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofwork;

        public CategoryController(IUnitofWork db)
        {
            _unitofwork = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitofwork.Category.GetAll();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Dis play order cannot exatly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["succes"] = "Category created succes";
                return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            }
            return View(obj);

        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitofwork.Category.GetFirstorDefault(c => c.Id == id);
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Dis play order cannot exatly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["succes"] = "Category edited succes";
                return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            }
            return View(obj);

        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitofwork.Category.GetFirstorDefault(c => c.Id == id);
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _unitofwork.Category.GetFirstorDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Remove(obj);
            _unitofwork.Save();
            TempData["succes"] = "Category deleted succes";
            return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---


        }
    }
}
