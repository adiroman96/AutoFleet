using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class ITP : Insurance
    {
        public ITP()
        {
            this.TypeOfInsurance = "ITP";
            this.Availability = 2;
        }
    }
}
