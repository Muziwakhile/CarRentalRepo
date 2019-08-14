//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRental3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.Bookings = new HashSet<Booking>();
        }
    
        public int Car_Id { get; set; }
        public string Registration { get; set; }
        public string brand { get; set; }
        public string Model { get; set; }
        public Nullable<int> SeatNumber { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> Category_Id { get; set; }
        public Nullable<int> CarStatus_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual Car_Category Car_Category { get; set; }
        public virtual Car_Status Car_Status { get; set; }
    }
}