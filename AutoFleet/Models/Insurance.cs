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
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tip")]
        public string TypeOfInsurance;

        [Required]
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

        [Display(Name = "Data expirare")]
        public DateTime ExpirationDate { get; private set; }

        [Display(Name = "Valabilitate")]
        public virtual int Availability { get; set; }
    }
}
