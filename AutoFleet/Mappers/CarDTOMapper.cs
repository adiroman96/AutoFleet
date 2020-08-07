using AutoFleet.Dtos;
using AutoFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Mappers
{
    public class CarDTOMapper
    {
        public static CarDTO CarAndDriverToCarDTO(Car car, Driver driver, List<Driver> availableDrivers)
        {
            CarDTO carDTO = new CarDTO();

            // setting Car data
            carDTO.CarId = car.Id;
            carDTO.CarManufacturingYear = car.ManufacturingYear;
            carDTO.CarRegistrationNumber = car.RegistrationNumber;

            // setting Insurances
            carDTO.Insurances = car.Insurances;

            // setting Driver data
            if (driver != null)
            {
                carDTO.DriverId = driver.Id;
                carDTO.DriverText = getDriverTextFromDriver(driver);
            }
            else
            {
                carDTO.DriverId = null;
                carDTO.DriverText = "";
            }

            // setting available drivers
            if (availableDrivers.Count > 0)
            {
                availableDrivers.ForEach(driver =>
                {
                    carDTO.AvailableDrivers.Add(new DriverDto
                    {
                        Id = driver.Id,
                        Text = getDriverTextFromDriver(driver)
                    });
                });
            }

            return carDTO;
        }

        public static Car CarDtoToCar(CarDTO dto)
        {
            Car car = new Car()
            {
                Id = dto.CarId,
                ManufacturingYear = dto.CarManufacturingYear,
                RegistrationNumber = dto.CarRegistrationNumber,                
                Insurances = dto.Insurances
            };
            return car;
        }
        private static string getDriverTextFromDriver(Driver driver)
        {
            return driver.Name + " (" + driver.Email + ")";
        }

    }

    

}
