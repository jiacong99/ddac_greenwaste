using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Models
{

    public class WasteServices
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string servicesTitle { get; set; }

        [StringLength(100)]
        [Display(Name = "Description")]
        public string serviceDescription { get; set; }

        [Display(Name = "Media URL")]
        public string serviceMediaURL { get; set; }

        [Required]
        [Display(Name = "Size")]
        public string serviceSize { get; set; }

    }
}
