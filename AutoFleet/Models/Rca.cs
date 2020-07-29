using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Rca : Insurance
    {
        private string typeOfInsurance;

        public override int Availability { get; set; } = 1;

        public new string TypeOfInsurance
        {
            get => typeOfInsurance;
            set
            {
                typeOfInsurance = "RCA";
            }
        }
    }
}
