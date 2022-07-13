using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Get user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            object[] props = { new Product() };
            shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCartRepository.GetAll(
                    u => u.ApplicationUserId == claim.Value, props)
            };
            
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQntd(
                    cart.Count, 
                    cart.Product.Price, 
                    cart.Product.Price50, 
                    cart.Product.Price100
                );

                shoppingCartVM.CartTotal += ( cart.Price * cart.Count );
            }
            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult addItem(int cartId) 
        {
            var cart = _unitOfWork.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCartRepository.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult subtractItem(int cartId) 
        {
            var cart = _unitOfWork.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCartRepository.Remove(cart);
            }
            else
            {
                _unitOfWork.ShoppingCartRepository.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult removeItem(int cartId) 
        {
            var cart = _unitOfWork.ShoppingCartRepository.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCartRepository.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private double GetPriceBasedOnQntd(double quantity, double price, double price50, double price100) 
        {
            if (quantity <= 50)
            {
                return price;
            }
            else if (quantity <= 100)
            {
                return price50;
            }
            return price100;
        }
    }
}