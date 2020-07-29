using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Rovinieta : Insurance
    {        
        public Rovinieta()
        {
            this.TypeOfInsurance = "Rovinieta";
            this.Availability = 1;
        }
    }
}
