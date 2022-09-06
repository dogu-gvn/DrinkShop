using FYD.DataAccess.Repository.IRepository;
using FYD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinddYourDrink.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitofWork db, IWebHostEnvironment hostEnvironment)
        {
            _unitofwork = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitofwork.Category.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                DrinkTypeList = _unitofwork.DrinkType.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })
            };
            if (id == null || id == 0)
            {
                // create product
                //ViewBag.CategoryList = CategoryList;
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitofwork.Product.GetFirstorDefault(u => u.Id == id);
                //update product
                return View(productVM);
            }


        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImageUrl != null)//delete image url
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;// data base string value
                }
                if (obj.Product.Id == 0)
                {
                    _unitofwork.Product.Add(obj.Product);
                }
                else
                {
                    _unitofwork.Product.Update(obj.Product);
                }

                _unitofwork.Save();
                TempData["succes"] = "Product created succesfully";
                return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            }
            return View(obj);

        }




        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitofwork.Product.GetAll(includeProperties: "Category,DrinkType");
            return Json(new { data = productList });
        }
        //Post
        [HttpDelete]


        public IActionResult Delete(int? id)
        {
            var obj = _unitofwork.Product.GetFirstorDefault(c => c.Id == id);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (obj == null)
            {
                return Json(new { succes = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();
            //return RedirectToAction("Index");//if we use diffrent controller we must use like ---RedirectToAction("Index","Control name here");---
            return Json(new { succes = true, message = "Delete Succes" });


        }

        #endregion
    }
}

