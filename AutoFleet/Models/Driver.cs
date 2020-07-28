using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume si prenume")]
        public string Name { get; set; }

        [Display(Name = "e-mail")]
        public string Email { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
