using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental3.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CarRental3.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking

        [Authorize(Users = "Mbulelon@gmail.com")]
        public ActionResult Index()
        {
            List<BookingModel> bookingList = new List<BookingModel>();

            using (DbEntities entities = new DbEntities())
            {
                var result = (from c in entities.Bookings
                              select c).ToList();

                foreach (var item in result)
                {
                    bookingList.Add(new BookingModel
                    {
                       BookingID = item.Booking_Id,
                       ClientID = item.ClientID,
                       Name = item.Client_Name,
                       PIN = item.Clinet_PIN,
                       CarId = item.Car_Id,
                       LicenceNumber = item.DriversLicentNumber,
                       DateFrom = item.DateFrom,
                       DateTo = item.DateTo,
                       Destination = item.Destination.Destination_Name,
                       BookingStatus = item.Booking_Status.BookingStatus_Name
                    });
                }
            }
            return View(bookingList);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Create(int id)
        {

            TempData["CarID"] = id;
         
            using (DbEntities entities = new DbEntities())
            {
                var result = (from i in entities.Destinations
                              select i).ToList<Destination>();

                ViewData["Destination"] = new SelectList(result, "Destination_Id", "Destination_Name");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(BookingDto booking)
        {
            string name;
            string surname;
            string sendto;
            string _user;
            if (booking.DateFrom < DateTime.Now )
            {
                ViewBag.Error = "Date Cannot Be Less Than Today's Date";
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            else if (booking.DateFrom > booking.DateTo || booking.DateTo < DateTime.Now)
            {
                ViewBag.Error = "Invalid Date range";
                return PartialView("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (ApplicationDbContext s = new ApplicationDbContext())
                {
                    string userID = User.Identity.GetUserId();
                    var result = (from u in s.Users
                                  where u.Id == (string)userID
                                  select u).SingleOrDefault();
                    booking.ClientID = result.Id;
                    booking.Name = result.Name;
                    name = result.Name;
                    surname = result.Surname;
                    sendto = ConfigurationManager.AppSettings["AdminMail"].ToString(); ;
                     _user = result.UserName;
                }

                using (DbEntities entities = new DbEntities())
                {

                    int carId = (int)TempData["CarID"];
                    var carResult = (from c in entities.Cars
                                     where c.Car_Id == carId
                                     select c).SingleOrDefault();
                    booking.CarId = carResult.Car_Id;
                    Booking bk = new Booking
                    {
                        Client_Name = booking.Name,
                        ClientID = booking.ClientID,
                        Clinet_PIN = booking.PIN,
                        DriversLicentNumber = booking.LicenceNumber,
                        Car_Id = booking.CarId,
                        DateFrom = booking.DateFrom,
                        DateTo = booking.DateTo,
                        DestinatioID = booking.Destination_Id,
                        BookingStatus = 1
                    };
                    entities.Bookings.Add(bk);
                    entities.SaveChanges();

                
                    //string message = $"<p>Dear Admin <br /><br />{name} {surname} has reserverd a car. please see booking Details Below<br />Booking ID:{bk.Booking_Id} <br /><br /> User : {_user}<br />Date From {booking.DateFrom} To {booking.DateTo}<br />Car Registration {carResult.Registration}<br /><br />regards<br /><br /> {name} {surname} </p>";
                    //string Mailsubject = "Car Reservation";
                    
                    //string mailFrom =  ConfigurationManager.AppSettings["EmailFrom"].ToString();
                    //string password = ConfigurationManager.AppSettings["EmailPass"].ToString();
                    //string host = ConfigurationManager.AppSettings["Host"].ToString();
                    //int port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());

                    //SmtpClient client = new SmtpClient(host, port);
                    //client.EnableSsl = true;
                    //client.Timeout = 100000;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = false;
                    //client.Credentials = new NetworkCredential(mailFrom, password);

                    //MailMessage mailMessage = new MailMessage(mailFrom,sendto,Mailsubject,message);
                    //mailMessage.IsBodyHtml = true;
                    //mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                    //client.Send(mailMessage);
                    

                    return RedirectToAction("Index", "Home");
                }
            }
           
        }

        [Authorize(Users = "Mbulelon@gmail.com")]
        [HttpGet]
        public ActionResult EditBookings(int ID)
        {

            
            BookingDto bookingDto = new BookingDto();
            using (DbEntities entities = new DbEntities())
            {
                var bookingresult = (from b in entities.Bookings
                                     where b.Booking_Id == ID
                                     select b).SingleOrDefault();
                ViewData["DateF"] = new DateTime(((DateTime)(bookingresult.DateFrom)).Year, ((DateTime)(bookingresult.DateFrom)).Month, ((DateTime)(bookingresult.DateFrom)).Day);
                ViewData["DateT"] = new DateTime(((DateTime)(bookingresult.DateTo)).Year, ((DateTime)(bookingresult.DateTo)).Month, ((DateTime)(bookingresult.DateTo)).Day);
                bookingDto.BookingID = bookingresult.Booking_Id;
                bookingDto.ClientID = bookingresult.ClientID;
                bookingDto.Name = bookingresult.Client_Name;
                bookingDto.PIN = bookingresult.Clinet_PIN;
                bookingDto.LicenceNumber = bookingresult.DriversLicentNumber;
                bookingDto.CarId = bookingresult.Car_Id;
                bookingDto.Destination_Id = bookingresult.DestinatioID;
                bookingDto.DateFrom = bookingresult.DateFrom;
                bookingDto.DateTo = bookingresult.DateTo;
                bookingDto.BookingStatus = bookingresult.BookingStatus;

                var destin = (from i in entities.Destinations
                              select i).ToList<Destination>();

                var status = (from i in entities.Booking_Status
                              select i).ToList<Booking_Status>();
                ViewData["Status"] = new SelectList(status, "BookingStatus_Id", "BookingStatus_Name");
                ViewData["Destination"] = new SelectList(destin, "Destination_Id", "Destination_Name");
            }
            return View(bookingDto);
        }

        [Authorize(Users = "Mbulelon@gmail.com")]
        [HttpPost]
        public ActionResult EditBookings(BookingDto bookingDto)
        {
            using (DbEntities entities = new DbEntities())
            {
                var bresult = (from b in entities.Bookings
                               where b.Booking_Id == bookingDto.BookingID
                               select b).SingleOrDefault();
                var cresults = (from c in entities.Cars
                                where c.Car_Id == bookingDto.CarId
                                select c).SingleOrDefault();

                if (bookingDto.BookingStatus == 3 || bookingDto.BookingStatus == 4 )
                {
                    cresults.CarStatus_Id = 3;
                    bresult.BookingStatus = bookingDto.BookingStatus;
                    entities.SaveChanges();
                }
                else if (bookingDto.BookingStatus == 2)
                {
                    cresults.CarStatus_Id = 1;
                    bresult.BookingStatus = bookingDto.BookingStatus;
                    entities.SaveChanges();
                }
                else
                {
                    bresult.BookingStatus = bookingDto.BookingStatus;
                    entities.SaveChanges();
                }

               
            }

           return RedirectToAction("Index");
        }
    }
}