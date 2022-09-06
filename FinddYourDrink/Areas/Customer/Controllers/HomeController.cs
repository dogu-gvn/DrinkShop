using FYD.DataAccess.Repository.IRepository;
using FYD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FYD.Models.ViewModels;

namespace FinddYourDrink.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IUnitofWork _unitofwork;
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofwork)
        {
            _logger = logger;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofwork.Product.GetAll(includeProperties: "Category,DrinkType");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitofwork.Product.GetFirstorDefault(c => c.Id == id, includeProperties: "Category,DrinkType")
            };
            return View(cartObj);
        }
        public IActionResult cartObj()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

    }
}