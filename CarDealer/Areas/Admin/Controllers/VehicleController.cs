using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.ViewModels;
using CarDealer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private IWebHostEnvironment _webHostEnvironment;


        public VehicleController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Vehicle> VehicleList = _unitOfWork.Vehicle.GetAll().ToList();
            return View(VehicleList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Vehicle Vehicle)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicle.Add(Vehicle);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle created successfully";
            }
            else {
                TempData["error"] = "Error creating Vehicle";
            }

           
            return RedirectToAction("Index");

        }


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
        public IActionResult Upsert(VehicleVM _vehicleVm, IFormFile? file) {
            if (ModelState.IsValid) {

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                { 
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath,@"images\vehicles");
                    var extension = Path.GetExtension(file.FileName);

                }
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
            Vehicle? VehiclefromDb = _unitOfWork.Vehicle.Get(x => x.Id == id);

            if (VehiclefromDb == null)
            {
                return NotFound();
            }
            return View(VehiclefromDb);
        }

        [HttpPost]
        public IActionResult Edit(Vehicle Vehicle)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicle.Update(Vehicle);
                _unitOfWork.Save();
            }

            TempData["success"] = "Vehicle updated successfully";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            Vehicle? VehiclefromDb = _unitOfWork.Vehicle.Get(x => x.Id == id);
            if (VehiclefromDb == null)
            {
                return NotFound();
            }
            return View(VehiclefromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Vehicle? VehicleFromDb = _unitOfWork.Vehicle.Get(x => x.Id == id);
            if (VehicleFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Vehicle.Remove(VehicleFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Vehicle deleted successfully";
            return RedirectToAction("Index");
        }



        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var VehicleList = _unitOfWork.Vehicle.GetAll(includeProperties: "Model,Model.Make");
            return Json(new { data = VehicleList });
        }

        #endregion

    }
}
