using CarDealer.Models;
using CarDealer.Models.ViewModels;
using CarDealer.Repository.Interfaces;
using CarDealer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop.Implementation;
using System.Data;

namespace CarDealer.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = CarDealerRoles.Role_Admin)]
    public class VehicleModelController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;

        public VehicleModelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region HTTP_GET_POST
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VehicleModelVM myModel = new()
            {
                VehicleModel = new(),
                MakeList = _unitOfWork.Make.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(myModel);
            }

            myModel.VehicleModel = _unitOfWork.VehicleModel.Get(u => u.Id == id);
            return View(myModel);
        }



        [HttpPost]
        public IActionResult Upsert(VehicleModelVM _vehicleModel)
        {
            if (ModelState.IsValid)
            {
                if (_vehicleModel.VehicleModel.Id == 0)
                    _unitOfWork.VehicleModel.Add(_vehicleModel.VehicleModel);
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
        #endregion


        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.VehicleModel.GetAll(includeProperties: "Make");
            return Json(new { data = modelList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var modelToDelete = _unitOfWork.VehicleModel.Get(u => u.Id == id);

            if (modelToDelete == null)
                return Json(new { success = false, message = "Error while deleting" });

            _unitOfWork.VehicleModel.Remove(modelToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });
        }


        #endregion


    }
}
