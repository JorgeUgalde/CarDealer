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
            List<Vehicle> vehicleList = _unitOfWork.Vehicle.GetAll().ToList();

            return View(vehicleList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            // Esto para validar que no exista
            //vehicle find = _db.vehicles.FirstOrDefault(vehicle);
            //if (find == null)
            //{ }
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicle.add(vehicle);
                _unitOfWork.Save();
            }

            TempData["success"] = "Vehicle created succesfully";


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

            Vehicle? vehicleFromDB = _unitOfWork.Vehicle.Get(x => x.Id == id);

            //Otras formas de traer objetos
            //vehicle? vehicle1 = db.vehicles.FirstOrDefault(x => x.Name == "Audi");
            //vehicle? vehicle2 = db.vehicles.Where(x => x.Id == id).FirstOrDefault();

            //Valido si el Id existe en la BD
            if (vehicleFromDB == null)
            {
                return NotFound();
            }

            return View(vehicleFromDB);
        }


        [HttpPost]
        public IActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicle.Update(vehicle);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle edited succesfully";
            }
            else
            {
                TempData["error"] = "Error editing vehicle";
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

            Vehicle? vehicleFromDB = _unitOfWork.Vehicle.Get(x => x.Id == id);

            //Valido si el Id existe en la BD
            if (vehicleFromDB == null)
            {
                return NotFound();
            }

            return View(vehicleFromDB);
        }



        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? Id)
        {
            Vehicle? vehicleFromDB = _unitOfWork.Vehicle.Get(x => x.Id == Id);
            if (vehicleFromDB == null)
            {
                TempData["error"] = "Error deleting vehicle";
                return NotFound();

            }
            _unitOfWork.Vehicle.remove(vehicleFromDB);
            _unitOfWork.Save();
            TempData["success"] = "vehicle deleted succesfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VehicleVM myModel = new()
            {
                Vehicle = new(),
                VehicleModelList = _unitOfWork.VehicleModel.GetAll(includeProperties: "Make").Select(i => new SelectListItem
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
        public IActionResult Upsert(VehicleVM? vehicleVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        //generar un GUID
                        String fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images\vehicles");
                        var extension = Path.GetExtension(file.FileName);
                    }
                }
            }


            return RedirectToAction();
        }

                    //    _unitOfWork.Vehicle.add(vehicle);
                    //    _unitOfWork.Save();
                    //    TempData["success"] = "Vehicle created succesfully";
                    //}
                    //else
                    //{
                    //    TempData["error"] = "Error creating Vehicle";
                    //}
                    //return RedirectToAction("Index");


                    //if (ModelState.IsValid)
                    //{
                    //    vehicleFromDB.PictureURL = vehicle.PictureURL;
                    //    vehicleFromDB.Price = vehicle.Price;
                    //    vehicleFromDB.Description = vehicle.Description;
                    //    vehicleFromDB.PictureURL = vehicle.PictureURL;
                    //    _unitOfWork.Vehicle.Update(vehicleFromDB);
                    //    _unitOfWork.Save();
                    //    TempData["success"] = "Vehicle edited succesfully";
                    //}
                    //else
                    //{
                    //    TempData["error"] = "Error editing Vehicle";
                    //}

                    //return RedirectToAction("Index");

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var vehicleList = _unitOfWork.Vehicle.GetAll("Model,Model.Make");
            return Json(new { data = vehicleList });
        }

        #endregion
    }

}
