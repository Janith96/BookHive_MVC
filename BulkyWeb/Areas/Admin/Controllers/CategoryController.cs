using BookHive.DataAccess.Data;
using BookHive.DataAccess.Repository.IRepository;
using BookHive.Models;
using BookHive.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookHiveWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)] //only allow access to admins
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newRow)
        {
            if (newRow.Name.ToLower() == newRow.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be the same");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(newRow);
                _unitOfWork.Save();
                TempData["success"] = "The category was created successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var uneditedRow = _unitOfWork.Category.Get(u => u.Id == id);

            if (uneditedRow == null)
                return NotFound();

            return View(uneditedRow);
        }

        [HttpPost]
        public IActionResult Edit(Category updatedRow)
        {
            if (updatedRow.Name.ToLower() == updatedRow.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be the same");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(updatedRow);
                _unitOfWork.Save();
                TempData["success"] = "The category was edited successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var selectedRow = _unitOfWork.Category.Get(u => u.Id == id);

            if (selectedRow == null)
                return NotFound();

            return View(selectedRow);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            var selectedRow = _unitOfWork.Category.Get(u => u.Id == id);

            if (selectedRow == null)
                return NotFound();

            _unitOfWork.Category.Remove(selectedRow);
            _unitOfWork.Save();
            TempData["success"] = "The category was removed successfully.";
            return RedirectToAction("Index");
        }

    }
}
