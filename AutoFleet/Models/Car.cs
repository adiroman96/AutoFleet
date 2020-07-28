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
        public string RegistrationNumber { get; set; }

        [Required]
        [Range(1880, 9999)]
        public int ManufacturingYear
        {
            get
            {
                return ManufacturingYear;
            }
            set
            {
                if (value < 1880 || value > DateTime.Now.Year)
                {
                    throw new ArgumentOutOfRangeException("ManufacturingYear", "ManufacturingYear must be a value between 1880 and 9999");
                }
                ManufacturingYear = value;
            }
        }

        public List<Insurance> Insurances { get; set; } = new List<Insurance>();
    }
}
