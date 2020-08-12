using System;


namespace AutoFleet.Dtos
{
    public class InsuranceDTO
    {
        public int Id { get; set; }

        public string TypeOfInsurance { get; set; }

        public DateTime LastRenewal { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ReminderInterval { get; set; }
    }
}
