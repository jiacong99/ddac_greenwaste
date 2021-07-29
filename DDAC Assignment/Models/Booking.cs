using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Models
{
    public class Booking // make table structure, table name - Booking
    {
        public int BookingID { get; set; }
        [Required]
        [Display(Name = "Service Type")]
        public string BookingType { get; set; }
        [Required]
        [Display(Name = "Service Price RM")]// service type //dropdown list
        [Column(TypeName = "decimal(18,2)")]
        public decimal BookingPrice { get; set; } // Service Price // dropdown list
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }
        [Required]
        [Display(Name = "Booking Location")]
        public string BookingLocation { get; set; }
        public string BookingStatus { get; set; } //no need show
        public string DriverName { get; set; } //no need show
    }
}
