using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Models
{
    public class DriverEntity : TableEntity
    {
        public DriverEntity(string DriverFirstName, string DriverLastName)
        {
            this.PartitionKey = DriverFirstName;
            this.RowKey = DriverLastName;
        }
        public DriverEntity()
        {

        }

        //create other properties
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
