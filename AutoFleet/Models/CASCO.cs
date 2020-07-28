using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class CASCO : Insurance
    {
        public override int Availability { get; set; } = 1;
    }
}
