using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental3.Models
{
    public class BookingDto
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Display(Name = "Client")]
        public string ClientID { get; set; }
        public long? PIN { get; set; }
        public string Name { get; set; }
        public int? LicenceNumber { get; set; }

        [Display(Name = "Car ID")]
        public int? CarId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        [Display(Name = "Destination")]
        public int? Destination_Id { get; set; }

        [Display(Name = "Status")]
        public int? BookingStatus { get; set; }
    }
}