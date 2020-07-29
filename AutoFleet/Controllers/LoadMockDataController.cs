using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoFleet.Data;
using AutoFleet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoFleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoadMockDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoadMockDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            Car car = new Car();
            car.ManufacturingYear = 2010;
            car.RegistrationNumber = "SB" + "11" + "ABC";

            Insurance itp = new ITP();

            CultureInfo ci = CultureInfo.InvariantCulture;
            itp.LastRenewal = DateTime.ParseExact("12/25/2008", "MM/dd/yyyy", ci);
            car.Insurances.Add(itp);

            _context.Add(car);


            await _context.SaveChangesAsync();
            return new JsonResult("Obiecte adaugate");
        }
    }
}
