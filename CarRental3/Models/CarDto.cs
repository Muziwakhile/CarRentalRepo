using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental3.Models
{
    public class CarDto
    {
        public int ID { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Brand { get; set; }
        public string Model { get; set; }

        [Display(Name = "Number of Seats")]
        public int? SeatsNumber { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int? CarCategory_Id { get; set; }

        [Display(Name = "Status")]
        public int? CarStatus_Id { get; set; }
        public byte[] Image { get; set; }

        [Required]
        public HttpPostedFileWrapper ImagePath { get; set; }
    }
}