using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            Object[] opts = { new Category(), new CoverType() };
            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetAll(opts);
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            object[] opts = { new Category(), new CoverType() };
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.Id == id, opts)
            };
            return View(cartObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}