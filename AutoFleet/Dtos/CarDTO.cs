using AutoFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Dtos
{
    public class CarDTO
    {
        public int IdCar { get; set; }

        public string CarRegistrationNumber { get; set; }

        public int CarManufacturingYear { get; set; }

        public int? IdDriver { get; set; }

        public string DriverName { get; set; }

        public string DriverEmail { get; set; }

        public List<Insurance> insurances { get; set; } = new List<Insurance>();
    }
}
