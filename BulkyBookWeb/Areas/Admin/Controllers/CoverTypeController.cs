using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverTypeRepository.GetAll();
            return View(objCoverTypeList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository.Update(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type updated successfully";
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return Delete(coverTypeFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {
            _unitOfWork.CoverTypeRepository.Remove(coverType);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
