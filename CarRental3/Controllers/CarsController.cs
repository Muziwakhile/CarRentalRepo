using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental3.Models;

namespace CarRental3.Controllers
{
    [Authorize(Users ="Mbulelon@gmail.com")]
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index()
        {
            List<CarModel> carList = new List<CarModel>();
            using (DbEntities entities = new DbEntities())
            {

                var result = (from c in entities.Cars
                           select c).ToList();

                foreach (var item in result)
                {
                    carList.Add(new CarModel() {
                        ID = item.Car_Id,
                        Registration = item.Registration,
                        Brand = item.brand,
                        Model = item.Model,
                        SeatsNumber = item.SeatNumber,
                        Description = item.Description,
                        CarCategory = item.Car_Category.CarCategory_Name,
                        CarStatus = item.Car_Status.CarStatus_Name
                    });
                }
            }
            return View(carList);
        }

        [HttpGet]
        public ActionResult CreateCars()
        {
            List<Car_Category> catList = new List<Car_Category>();
            List<Car_Status> statusList = new List<Car_Status>();
            using (DbEntities entities = new DbEntities())
            {
                catList = (from l in entities.Car_Category
                           select l).ToList<Car_Category>();

            }
            using (DbEntities entities = new DbEntities())
            {
                statusList = (from s in entities.Car_Status
                              select s).ToList<Car_Status>();

            }
            ViewData["Department"] = new SelectList(catList, "CarCategory_Id", "CarCategory_Name");
            ViewData["Status"] = new SelectList(statusList, "CarStatus_Id", "CarStatus_Name");
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult CreateCars(CarDto carmodel)
        {
            var file = carmodel.ImagePath;

            BinaryReader reader = new BinaryReader(file.InputStream);
            byte[] img = reader.ReadBytes(file.ContentLength);

            using (DbEntities entities = new DbEntities())
            {
                Car c = new Car()
                {
                    Registration = carmodel.Registration,
                    Model = carmodel.Model,
                    brand = carmodel.Brand,
                    SeatNumber = carmodel.SeatsNumber,
                    Description = carmodel.Description,
                    Image = img,
                    Category_Id = carmodel.CarCategory_Id,
                    CarStatus_Id = carmodel.CarStatus_Id
                };
                entities.Cars.Add(c);
                entities.SaveChanges();
            }
            TempData.Keep();
            return RedirectToAction("Index");
        }

        public ActionResult Displayimage()
        {
            var file = Request.Files["Img"];
            if (file == null)
            {
                return null;
            }
            else
            {
                BinaryReader reader = new BinaryReader(file.InputStream);
                byte[] image = reader.ReadBytes(file.ContentLength);
                return File(image, "image/jpeg");
            }
            //BinaryReader reader = new BinaryReader(file.InputStream);
            //byte[] image = reader.ReadBytes(file.ContentLength);
            //return File(image, "image/jpeg");
        }

        [HttpGet]
        public ActionResult EditCar(int id)
        {
            TempData.Keep();

            using (DbEntities entity = new DbEntities())
            {
                var Result = (from c in entity.Cars
                                 where c.Car_Id == id
                                 select c).SingleOrDefault<Car>();

                CarDto cmodel = new CarDto
                {
                    ID = Result.Car_Id,
                    Registration = Result.Registration,
                    Model = Result.Model,
                    Brand = Result.brand,
                    Description = Result.Description,
                    SeatsNumber = Result.SeatNumber,
                    Image = Result.Image,
                    CarCategory_Id = Result.Category_Id,
                    CarStatus_Id = Result.CarStatus_Id,
                    
                };
                List<Car_Category> catList = new List<Car_Category>();
                List<Car_Status> statusList = new List<Car_Status>();
                using (DbEntities entities = new DbEntities())
                {
                    catList = (from l in entities.Car_Category
                               select l).ToList<Car_Category>();

                }
                using (DbEntities entities = new DbEntities())
                {
                    statusList = (from s in entities.Car_Status
                                  select s).ToList<Car_Status>();

                }
                TempData.Clear();
                ViewData["Department"] = new SelectList(catList, "CarCategory_Id", "CarCategory_Name", $"{cmodel.CarCategory_Id}");
                ViewData["Status"] = new SelectList(statusList, "CarStatus_Id", "CarStatus_Name", $"{ cmodel.CarStatus_Id }");


                return View(cmodel);
            }
            
        }

        [HttpPost]
        public ActionResult EditCar(CarDto carmodel)
        {
            if (carmodel.ImagePath != null)
            {
                var file = carmodel.ImagePath;

                BinaryReader reader = new BinaryReader(file.InputStream);
                byte[] img = reader.ReadBytes(file.ContentLength);
                using (DbEntities entities = new DbEntities())
                {
                    var result = (from car in entities.Cars
                                  where car.Car_Id == carmodel.ID
                                  select car).SingleOrDefault();

                    result.Registration = carmodel.Registration;
                    result.Model = carmodel.Model;
                    result.brand = carmodel.Brand;
                    result.SeatNumber = carmodel.SeatsNumber;
                    result.Description = carmodel.Description;
                    result.Image = img;
                    result.Category_Id = carmodel.CarCategory_Id;
                    result.CarStatus_Id = carmodel.CarStatus_Id;

                   
                    entities.SaveChanges();
                   
                }
            }
            else
            {
                using (DbEntities entities = new DbEntities())
                {
                    var result = (from car in entities.Cars
                                  where car.Car_Id == carmodel.ID
                                  select car).SingleOrDefault();

                    result.Registration = carmodel.Registration;
                    result.Model = carmodel.Model;
                    result.brand = carmodel.Brand;
                    result.SeatNumber = carmodel.SeatsNumber;
                    result.Description = carmodel.Description;
                    result.Category_Id = carmodel.CarCategory_Id;
                    result.CarStatus_Id = carmodel.CarStatus_Id;
                    
                    
                    entities.SaveChanges();
                }
            }

            return RedirectToAction("Index");
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

         public static byte[] main;
        public void RetrieveImage2()
        {
          
                var image = Request.Files[0];
                BinaryReader reader = new BinaryReader(image.InputStream);
                byte[] img = reader.ReadBytes(image.ContentLength);

                var g = File(img, "image/jpeg");
                main = img;
                //return Json(new { image = g},JsonRequestBehavior.AllowGet); 

           
            //return null;
        }

        public ActionResult RetrieveImage3()
        {
                return File(main, "image/jpeg");    
        }

    }
}