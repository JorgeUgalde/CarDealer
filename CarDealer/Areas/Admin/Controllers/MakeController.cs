using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // Esto para validar que no exista
            //Make find = _db.Makes.FirstOrDefault(make);
            //if (find == null)
            //{ }
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.add(make);
                _unitOfWork.Save();
            }

            TempData["success"] = "Make created succesfully";


            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null || id <= 0)
            {
                return NotFound();
            }

            //si el id es valido, cargo la info de la bd

            Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == id);

            //Otras formas de traer objetos
            //Make? make1 = db.Makes.FirstOrDefault(x => x.Name == "Audi");
            //Make? make2 = db.Makes.Where(x => x.Id == id).FirstOrDefault();

            //Valido si el Id existe en la BD
            if (makeFromDB == null)
            {
                return NotFound();
            }

            return View(makeFromDB);
        }


        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.Update(make);
                _unitOfWork.Save();
                TempData["success"] = "Make edited succesfully";
            }
            else
            {
                TempData["error"] = "Error editing make";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id == null || id <= 0)
            {
                return NotFound();
            }

            //si el id es valido, cargo la info de la bd

            Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == id);

            //Valido si el Id existe en la BD
            if (makeFromDB == null)
            {
                return NotFound();
            }

            return View(makeFromDB);
        }



        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? Id)
        {
            Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == Id);
            if (makeFromDB == null)
            {
                TempData["error"] = "Error deleting make";
                return NotFound();

            }
            _unitOfWork.Make.remove(makeFromDB);
            _unitOfWork.Save();
            TempData["success"] = "Make deleted succesfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id != null)
            {

                Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == id);

                //Valido si el Id existe en la BD
                if (makeFromDB == null)
                {
                    return NotFound();
                }
                TempData["edit/create"] = "Edit";
                return View(makeFromDB);
            }

            TempData["edit/create"] = "Create";
            return View();
        }


        [HttpPost]
        public IActionResult Upsert(Make? make)
        {
            Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == make.Id);
            // create
            if (makeFromDB == null)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Make.add(make);
                    _unitOfWork.Save();
                    TempData["success"] = "Make created succesfully";
                }
                else
                {
                    TempData["error"] = "Error creating make";
                }
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    makeFromDB.Name = make.Name;
                    _unitOfWork.Make.Update(makeFromDB);
                    _unitOfWork.Save();
                    TempData["success"] = "Make edited succesfully";
                }
                else
                {
                    TempData["error"] = "Error editing make";
                }
               
                return RedirectToAction("Index");
            }
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
