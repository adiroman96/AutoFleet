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

            // transforming Insurances in InsuranceDTO and adding them to the CarDTO
            car.Insurances.ForEach(x => carDTO.Insurances.Add(InsuranceDTOMapper.InsuranceToInsuranceDTO(x)));

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

        /*
         * in: carDto : CarDTO
         * returns a car with carDto values
         * 
         * throws Exception if in the carDto list of insurances there are at least two insurances with the same type or any of the items of insurances list has a null as value for TypeOfInsurance
         * 
         */
        public static Car CarDtoToCar(CarDTO dto)
        {
            // mapping car specific information
            Car car = new Car()
            {
                Id = dto.CarId,
                ManufacturingYear = dto.CarManufacturingYear,
                RegistrationNumber = dto.CarRegistrationNumber,
                DriverId = dto.DriverId
            };

            // mapping insurances
            foreach (var insuranceDto in dto.Insurances)
            {
                addInsuranceToCar(car, insuranceDto);                
            }

            // adding a new insurance if that is the case
            if(dto.NewInsurance.TypeOfInsurance != null)
            {
                addInsuranceToCar(car, dto.NewInsurance);
            }

            return car;
        }

        private static void addInsuranceToCar(Car car, InsuranceDTO insuranceDto)
        {
            Insurance insurance = createInsuranceInstance(insuranceDto);
            if (!existsAnInsuranceWithType(insurance.TypeOfInsurance, car.Insurances))
            {
                car.Insurances.Add(insurance);
            }
            else
            {
                throw new Exception("Nu pot exista doua asigurari de acelasi tip pentru aceasi amasina");
            }
        }

        /*
         * in:
         * a string that specifies the specific type of the insurance
         * a list of already existing insurances
         * out: 
         * true, there is at least one insurance with that type
         * false, else
         */
        private static bool existsAnInsuranceWithType(string typeOfInsurance, List<Insurance> insurances)
        {
            return insurances.Exists(insurance => insurance.TypeOfInsurance.CompareTo(typeOfInsurance).Equals(0));
        }

        /*
         * in: driver:Driver
         * returns: a string with following structure: "NAME (EMAIL)"
         */
        private static string getDriverTextFromDriver(Driver driver)
        {
            return driver.Name + " (" + driver.Email + ")";
        }

        /*
         * input: an insurance
         * returns:
         * an Insurance child (RCA, ITP, CASCO, ...) based on the value of TypeOfInsurance
         * 
         * Throws Exception  when typeOfInsurance is not a Child of Insurance
         */
        private static Insurance createInsuranceInstance(InsuranceDTO insurance)
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
                    throw new Exception("Tipul asigurarii nu este recunoscut.");
            }
            newInsurance.Id = insurance.Id;
            newInsurance.LastRenewal = insurance.LastRenewal;
            newInsurance.ReminderInterval = insurance.ReminderInterval;

            return newInsurance;
        }

    }





}
