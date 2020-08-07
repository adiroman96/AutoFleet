using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoFleet.Data;
using AutoFleet.Models;
using Microsoft.AspNetCore.Authorization;
using AutoFleet.Dtos;
using System.Security.Cryptography.X509Certificates;
using AutoFleet.Mappers;
using System.Globalization;

namespace AutoFleet.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationNumber,ManufacturingYear")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            var insurances = _context.Insurances.ToList<Insurance>();

            if (car == null)
            {
                return NotFound();
            }

            Driver driver = await GetDriver(car);
            List<Driver> availableDrivers = _context.Drivers.ToList<Driver>();
            CarDTO carDTO = CarDTOMapper.CarAndDriverToCarDTO(car, driver, availableDrivers);

            return View(carDTO);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId, CarRegistrationNumber, CarManufacturingYear, DriverId, Insurances")] CarDTO carDTO)
        {
            carDTO.Insurances.Remove(carDTO.Insurances.FindLast(i => i.TypeOfInsurance == null));

            if (id != carDTO.CarId)
            {
                return NotFound();
            }

            Car car = CarDTOMapper.CarDtoToCar(carDTO);

            try
            {
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }


        public async Task<Car> GetCar(int? id)
        {
            if (id == null)
            {
                throw new ObjectNotFoundException();
            }

            Car car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            var insurances = _context.Insurances.ToList<Insurance>();

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            return car;
        }

        public async Task<Driver> GetDriver(Car car)
        {
            Driver driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Cars.Contains<Car>(car));

            return driver;
        }

        [HttpGet]
        public async Task<IActionResult> CreateMock()
        {
            int nr = 28;

            Car car = new Car();
            car.ManufacturingYear = 2019;
            car.RegistrationNumber = "SB" + nr + "XXX";

            Insurance itp = new CASCO
            {
                LastRenewal = new DateTime(2019, 01, nr),
            };
            car.Insurances.Add(itp);

            _context.Add(car);
            //_context.Add(itp);

            Driver driver = new Driver();
            driver.Name = "Alex Mock";
            driver.Email = "mock" + nr + "@fakedomain.com";
            driver.Cars.Add(car);
            _context.Add(driver);


            await _context.SaveChangesAsync();
            return Json("Obiecte adaugate");
        }
    }
}
