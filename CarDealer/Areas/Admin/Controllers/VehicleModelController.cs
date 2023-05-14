using CarDealer.Models;
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



        [HttpGet]
        public IActionResult Create()
        {
            VehicleModelVM myModel = new VehicleModelVM();

            myModel.MakeList = _unitOfWork.Make.GetAll().ToList();

            return View(myModel);
        }


    }
}
