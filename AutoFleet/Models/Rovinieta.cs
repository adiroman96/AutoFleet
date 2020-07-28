using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Rovinieta : Insurance
    {
        public override int Availability { get; set; } = 1;
    }
}
