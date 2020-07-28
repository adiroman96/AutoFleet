﻿using AutoFleet.Dtos;
using AutoFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Mappers
{
    public class CarDTOMapper
    {
        public static CarDTO CarAndDriverToCarDTO(Car car, Driver driver)
        {
            CarDTO carDTO = new CarDTO();

            // setting Car data
            carDTO.IdCar = car.Id;
            carDTO.CarManufacturingYear = car.ManufacturingYear;
            carDTO.CarRegistrationNumber = car.RegistrationNumber;
            
            // setting Driver data
            if (driver != null)
            {
                carDTO.IdDriver = driver.Id;
                carDTO.DriverEmail = driver.Email;
                carDTO.DriverName = driver.Name;
            }
            else
            {
                carDTO.IdDriver = null;
                carDTO.DriverEmail = "";
                carDTO.DriverName = "";
            }

            // setting Insurances
            carDTO.insurances = car.Insurances;

            return carDTO;
        }

        //public static Car CarDTOToCar(CarDTO carDTO)
        //{
        //    var car = new Car();
        //    car.Id = carDTO.Id;
        //    car.ManufacturingYear = carDTO.CarManufacturingYear;
        //    car.RegistrationNumber = carDTO.CarRegistrationNumber;
        //    return car;
        //}
    }
}
