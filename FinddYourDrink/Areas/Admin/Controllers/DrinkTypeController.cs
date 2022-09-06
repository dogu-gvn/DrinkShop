using FYD.DataAccess.Repository.IRepository;
using FYD.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinddYourDrink.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DrinkTypeController:Controller
    {
        private readonly IUnitofWork _unitofwork;

        public DrinkTypeController(IUnitofWork db)
        {
            _unitofwork = db;
        }
        public IActionResult Index()
        {
            var objDrinkTypesList = _unitofwork.DrinkType.GetAll();
            return View(objDrinkTypesList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(DrinkType obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.DrinkType.Add(obj);
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
            var covertypeFromDb = _unitofwork.DrinkType.GetFirstorDefault(c => c.Id == id);
            return View(covertypeFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(DrinkType obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.DrinkType.Update(obj);
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
            var covertypeFromDb = _unitofwork.DrinkType.GetFirstorDefault(c => c.Id == id);
            return View(covertypeFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _unitofwork.DrinkType.GetFirstorDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.DrinkType.Remove(obj);
            _unitofwork.Save();
            TempData["succes"] = "Category deleted succes";
            return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---


        }
    }
}
