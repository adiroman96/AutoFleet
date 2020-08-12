using AutoFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Dtos
{
    public class CarDTO
    {
        public int CarId { get; set; }

        public string CarRegistrationNumber { get; set; }

        public int CarManufacturingYear { get; set; }

        public int? DriverId { get; set; }

        public string DriverText { get; set; }

        public List<InsuranceDTO> Insurances { get; set; } = new List<InsuranceDTO>();

        public List<DriverDto> AvailableDrivers { get; set; } = new List<DriverDto>();

    }
}
