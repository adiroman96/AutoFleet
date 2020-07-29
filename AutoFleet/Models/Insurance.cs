using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Insurance
    {
        private DateTime lastRenewal;
        private string typeOfInsurance;
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tip")]
        public string TypeOfInsurance {
            get => typeOfInsurance;
            protected set => typeOfInsurance = value;
        }

        [Display(Name = "Ultima reinnoire")]
        public DateTime LastRenewal
        {
            get => lastRenewal;
            
            set
            {
                lastRenewal = value;
                ExpirationDate = value.AddYears(Availability);
            }
        }

        // ExpirationDate is readonly as is calculated using the availability
        [Display(Name = "Data expirare")]
        public DateTime ExpirationDate { get; private set; }

        // public+recorded for each insurance because the availability can change in time as a scpecific car gets older
        [Display(Name = "Valabilitate")]
        public int Availability { get; set; }
    }
}
