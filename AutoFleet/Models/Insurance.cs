using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Models
{
    public class Insurance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ultima reinnoire")]
        public DateTime LastRenewal
        {
            get
            {
                return LastRenewal;
            }
            set
            {
                LastRenewal = value;
                ExpirationDate = value.AddYears(Availability);
            }
        }

        [Display(Name = "Data expirare")]
        public DateTime ExpirationDate { get; private set; }

        [Display(Name = "Valabilitate")]
        public virtual int Availability { get; set; }
    }
}
