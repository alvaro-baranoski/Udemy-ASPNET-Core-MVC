using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
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
            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetAll(includeProperties: opts);
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            object[] opts = { new Category(), new CoverType() };
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.Id == productId, opts)
            };
            return View(cartObj);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            var cartFromDb = _unitOfWork.ShoppingCartRepository.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId
            );

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartRepository.GetAll(
                    u => u.ApplicationUserId == claim.Value).ToList().Count
                );
            }
            else
            {
                _unitOfWork.ShoppingCartRepository.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
            }


            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}