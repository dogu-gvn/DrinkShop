using FYD.DataAccess.Repository.IRepository;
using FYD.DataAccess;
using FYD.Models;
using FYD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;

namespace FinddYourDrink.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitofwork;        
        public CompanyController(IUnitofWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            
            if (id == null || id == 0)
            {                
                return View(company);
            }
            else
            {
                company = _unitofwork.Company.GetFirstorDefault(u => u.Id == id);                
                return View(company);
            }


        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {                
                
                if (obj.Id == 0)
                {
                    _unitofwork.Company.Add(obj);
                }
                else
                {
                    _unitofwork.Company.Update(obj);
                }

                _unitofwork.Save();
                TempData["succes"] = "Company created succesfully";
                return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            }
            return View(obj);

        }




        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitofwork.Company.GetAll();
            return Json(new { data = companyList });
        }
        //Post
        [HttpDelete]


        public IActionResult Delete(int? id)
        {
            var obj = _unitofwork.Company.GetFirstorDefault(c => c.Id == id);
            
            if (obj == null)
            {
                return Json(new { succes = false, message = "Error while deleting" });
            }            
            _unitofwork.Company.Remove(obj);
            _unitofwork.Save();
            //return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            return Json(new { succes = true, message = "Delete Succes" });


        }

        #endregion
    }
}

