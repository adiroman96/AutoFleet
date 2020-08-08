using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Rca : Insurance
    {
        public Rca()
        {
            this.TypeOfInsurance = "Rca";
            this.Availability = 1;
        }
    }
}
