using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental3.Models
{
    public class CarModel
    {
        public int ID { get; set; }
        public string Registration { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Display(Name ="Number Of Seats")]
        public int? SeatsNumber { get; set; }
        public string Description { get; set; }
        [Display(Name = "Car Category")]
        public string CarCategory { get; set; }
        [Display(Name = "Car Status")]
        public string CarStatus { get; set; }
        public byte[] Image { get; set; }

    }
}