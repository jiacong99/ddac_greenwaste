using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Models
{

    public class WasteServices
    {
        [Key]
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

        [Required]
        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal serviceAmount { get; set; }
    }
}
