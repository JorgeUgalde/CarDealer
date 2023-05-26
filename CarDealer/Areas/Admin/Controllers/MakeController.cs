using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MakeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MakeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Make> makeList = _unitOfWork.Make.GetAll().ToList();
            return View(makeList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.Add(make);
                _unitOfWork.Save();
                TempData["success"] = "Make created successfully";
            }
            else {
                TempData["error"] = "Error creating make";
            }

           
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            Make? makefromDb = _unitOfWork.Make.Get(x => x.Id == id);

            if (makefromDb == null)
            {
                return NotFound();
            }
            return View(makefromDb);
        }

        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.Update(make);
                _unitOfWork.Save();
            }

            TempData["success"] = "Make updated successfully";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            Make? makefromDb = _unitOfWork.Make.Get(x => x.Id == id);
            if (makefromDb == null)
            {
                return NotFound();
            }
            return View(makefromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Make? makeFromDb = _unitOfWork.Make.Get(x => x.Id == id);
            if (makeFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Make.Remove(makeFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Make deleted successfully";
            return RedirectToAction("Index");
        }



        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var makeList = _unitOfWork.Make.GetAll();
            return Json(new { data = makeList });
        }

        #endregion

    }
}
