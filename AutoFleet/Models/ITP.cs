using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class ITP : Insurance
    {
        private string typeOfInsurance;

        public override int Availability { get; set; } = 2;

        public new string TypeOfInsurance
        {
            get => typeOfInsurance;
            set
            {
                typeOfInsurance = "ITP";
            }
        }
    }
}
