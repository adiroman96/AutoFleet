using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nr. inmatriculare")]
        public string RegistrationNumber { get; set; }

        [Required]
        [Range(1980, 9999)]
        [Display(Name = "An fabricatie")]
        public int ManufacturingYear
        {
            get; set;
        }

        public List<Insurance> Insurances { get; set; } = new List<Insurance>();

        public int? DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}
