using CarDealer.Models;
using CarDealer.Models.ViewModels;
using CarDealer.Repository;
using CarDealer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CarDealer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Vehicle> vehicleList = _unitOfWork.Vehicle.GetAll(includeProperties: "Model,Model.Make");

            return View(vehicleList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}