using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental3.Models
{
    public class CarBookingModel
    {
        public int ID { get; set; }
        public long PIN { get; set; }
        public string Name { get; set; }
        public long LicenceNumber { get; set; }
        public int CarId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        public int DestinationID { get; set; }
        public int BookingID { get; set; }
    }
}