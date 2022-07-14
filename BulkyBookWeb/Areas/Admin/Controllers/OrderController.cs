using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // Properties
        private readonly IUnitOfWork _unitOfWork; 

        // Constructor
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Methods
        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderHeader> orderHeaders;
            Object[] props = { new ApplicationUser() };
            orderHeaders = _unitOfWork.OrderHeaderRepository.GetAll(includeProperties: props);
            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}