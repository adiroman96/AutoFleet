using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class CASCO : Insurance
    {
        public CASCO()
        {
            this.TypeOfInsurance = "CASCO";
            this.Availability = 1;
        }
    }
}
