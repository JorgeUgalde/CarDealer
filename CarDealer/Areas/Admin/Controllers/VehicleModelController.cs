using CarDealer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarDealer.Models.ViewModels;
using CarDealer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class VehicleModelController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;

        public VehicleModelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get_Post

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VehicleVM myModel = new()
            {
                Vehicle = new(),
                VehicleModelList = _unitOfWork.VehicleModel.GetAll(includeProperties:"Make").Select(i => new SelectListItem
                {
                    Text = i.Make.Name + " " + i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(myModel);
            }
            myModel.Vehicle = _unitOfWork.Vehicle.Get(u => u.Id == id);
            return View(myModel);
        }



        [HttpPost]
        public IActionResult Upsert(VehicleModelVM _vehicleModel)
        {
            if (ModelState.IsValid)
            {
                if (_vehicleModel.VehicleModel.Id == 0)
                    _unitOfWork.VehicleModel.add(_vehicleModel.VehicleModel);
                else
                    _unitOfWork.VehicleModel.Update(_vehicleModel.VehicleModel);

                _unitOfWork.Save();
                TempData["success"] = "Model saved successfully";
            }
            else
            {
                TempData["error"] = "Error creating model";
            }


            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult Create(VehicleModelVM model)
        {
            // Esto para validar que no exista
            //Make find = _db.Makes.FirstOrDefault(make);
            //if (find == null)
            //{ }
            if (ModelState.IsValid)
            {
                _unitOfWork.VehicleModel.add(model.VehicleModel);
                _unitOfWork.Save();
                TempData["success"] = "Model created succesfully";
            }
            else
            {
                TempData["error"] = "Error creating model";
            }
            return RedirectToAction("Index");
        }

        #endregion


        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.VehicleModel.GetAll("Make");
            return Json(new { data = modelList });
        }


        [HttpDelete]

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var modelToDelete = _unitOfWork.VehicleModel.Get(u => u.Id == id);
            if (modelToDelete == null)
                return Json(new { success = false, message = "Error while deleting" });
            _unitOfWork.VehicleModel.remove(modelToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion

    }
}
