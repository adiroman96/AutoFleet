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
        private int reminderInterval = 10; // number of days

        public Insurance()
        {
            ExpirationDate = LastRenewal.AddYears(Availability);
        }

        public Insurance(int Id, DateTime LastRenewal)
        {
            this.Id = Id; 
            this.LastRenewal = LastRenewal;
            this.ExpirationDate = LastRenewal.AddYears(Availability);
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Tip")]
        public string TypeOfInsurance {
            get => typeOfInsurance;
            set => typeOfInsurance = value;
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
      
        public DateTime ReminderDate { get; private set; }

        // public+recorded for each insurance because the availability can change in time as a scpecific car gets older
        [Display(Name = "Valabilitate")]
        public int Availability { get; set; }

        public int ReminderInterval 
        {
            get => reminderInterval;
            set
            {
                reminderInterval = value;
                ReminderDate = ExpirationDate.AddDays(-reminderInterval);
            }
        }

        public int? CarId { get; set; }
    }
}
