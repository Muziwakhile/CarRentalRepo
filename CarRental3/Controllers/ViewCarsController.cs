using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental3.Models;

namespace CarRental3.Controllers
{
    public class ViewCarsController : Controller
    {
        // GET: ViewCars
       [HttpGet]
        public ActionResult Sedans()
        {
            List<CarModel> SedanList = new List<CarModel>();
            using (DbEntities entity = new DbEntities())
            {
                var result = (from s in entity.Cars
                              where s.Category_Id == 1 & s.CarStatus_Id == 3
                              select s).ToList<Car>();

                foreach (var item in result)
                {
                    SedanList.Add(new CarModel()
                    {
                        ID = item.Car_Id,
                        Registration = item.Registration,
                        Brand = item.brand,
                        Model = item.Model,
                        SeatsNumber = item.SeatNumber,
                        Description = item.Description,
                        CarCategory = item.Car_Category.CarCategory_Name,
                        CarStatus = item.Car_Status.CarStatus_Name,
                        Image = item.Image
                    });
                }
            }
            return View(SedanList);
        }


        [HttpGet]
        public ActionResult FamilyCars()
        {
            List<CarModel> FcarList = new List<CarModel>();
            using (DbEntities entity = new DbEntities())
            {
                var result = (from s in entity.Cars
                              where s.Category_Id == 2 & s.CarStatus_Id == 3
                              select s).ToList<Car>();

                foreach (var item in result)
                {
                    FcarList.Add(new CarModel()
                    {
                        ID = item.Car_Id,
                        Registration = item.Registration,
                        Brand = item.brand,
                        Model = item.Model,
                        SeatsNumber = item.SeatNumber,
                        Description = item.Description,
                        CarCategory = item.Car_Category.CarCategory_Name,
                        CarStatus = item.Car_Status.CarStatus_Name,
                        Image = item.Image
                    });
                }
            }
            return View(FcarList);
        }


        [HttpGet]
        public ActionResult Trucks()
        {
            List<CarModel> TruckList = new List<CarModel>();
            using (DbEntities entity = new DbEntities())
            {
                var result = (from s in entity.Cars
                              where s.Category_Id == 4 & s.CarStatus_Id == 3
                              select s).ToList<Car>();

                foreach (var item in result)
                {
                    TruckList.Add(new CarModel()
                    {
                        ID = item.Car_Id,
                        Registration = item.Registration,
                        Brand = item.brand,
                        Model = item.Model,
                        SeatsNumber = item.SeatNumber,
                        Description = item.Description,
                        CarCategory = item.Car_Category.CarCategory_Name,
                        CarStatus = item.Car_Status.CarStatus_Name,
                        Image = item.Image
                    });
                }
            }
            return View(TruckList);
        }

        public ActionResult RetrieveImage(int id)
        {
            using (DbEntities entity = new DbEntities())
            {
                var Result = (from c in entity.Cars
                              where c.Car_Id == id
                              select c).SingleOrDefault<Car>();

                return File(Result.Image, "image/jpeg");
            }
        }
    }
}