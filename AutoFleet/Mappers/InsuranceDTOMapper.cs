using AutoFleet.Dtos;
using AutoFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFleet.Mappers
{
    public static class InsuranceDTOMapper
    {
        public static InsuranceDTO InsuranceToInsuranceDTO(Insurance original)
        {
            InsuranceDTO dto = new InsuranceDTO()
            {
                Id = original.Id,
                TypeOfInsurance = original.TypeOfInsurance,
                LastRenewal = original.LastRenewal,
                ExpirationDate = original.ExpirationDate,
                ReminderInterval = original.ReminderInterval
            };

            return dto;
        }

        public static Insurance InsuranceDTOToInsurance(InsuranceDTO dto)
        {
            Insurance insurance = new Insurance()
            {
                Id = dto.Id,
                TypeOfInsurance = dto.TypeOfInsurance,
                LastRenewal = dto.LastRenewal,
                ReminderInterval = dto.ReminderInterval
            };

            return insurance;
        }
    }
}
