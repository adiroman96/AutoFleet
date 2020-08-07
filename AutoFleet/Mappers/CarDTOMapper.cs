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
                DriverId = dto.DriverId
            };

            foreach (var insurance in dto.Insurances)
            {
                Insurance newInsurance = createInsuranceInstance(insurance);
                if (newInsurance != null)
                    car.Insurances.Add(createInsuranceInstance(insurance));
            }

            return car;
        }

        private static string getDriverTextFromDriver(Driver driver)
        {
            return driver.Name + " (" + driver.Email + ")";
        }

        /*
         * recieves an insurance
         * and based on typeOfInsurance creates an instance of that type (RCA, ITP...) with given data
         * 
         * return null when typeOfInsurance is not a Child of Insurance
         */
        private static Insurance createInsuranceInstance(Insurance insurance)
        {
            Insurance newInsurance;
            switch (insurance.TypeOfInsurance)
            {
                case "Rovinieta":
                    newInsurance = new Rovinieta();
                    break;
                case "ITP":
                    newInsurance = new ITP();
                    break;
                case "CASCO":
                    newInsurance = new CASCO();
                    break;
                case "Rca":
                    newInsurance = new Rca();
                    break;
                default:
                    return null;                  
            }
            newInsurance.Id = insurance.Id;
            newInsurance.LastRenewal = insurance.LastRenewal;

            return newInsurance;
        }

    }





}
