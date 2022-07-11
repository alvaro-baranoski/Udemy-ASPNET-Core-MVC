using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CompanyUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            CompanyUser companyUser = new();

            if (id == null || id == 0)
            {
                return View(companyUser);
            }
            else
            {
                companyUser = _unitOfWork.CompanyUserRepository.GetFirstOrDefault(i => i.Id == id);
                return View(companyUser);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CompanyUser obj)
        {
            if (ModelState.IsValid)
            {
                // Add
                if (obj.Id == 0)
                {
                    _unitOfWork.CompanyUserRepository.Add(obj);
                    TempData["success"] = "Company user created successfully";

                }
                else
                {
                    _unitOfWork.CompanyUserRepository.Update(obj);
                    TempData["success"] = "Company user updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var objList = _unitOfWork.CompanyUserRepository.GetAll();
            return Json(new { data = objList });
        }

        // POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.CompanyUserRepository.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.CompanyUserRepository.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
