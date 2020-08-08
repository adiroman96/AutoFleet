using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutoFleet.Utils
{
    public class EmailHelper
    {
        const string mail = "ubb.cercc@gmail.com";
        const string password = "macppnet";

        public static void SendMail(string nameOfRecipient, string emailOfRecipient, string expiringObject, string finalDate, string registrationNumber)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = "URGENT! Mentenanta masina!",
                Body = "<p><strong>Expira " + expiringObject + "</strong></p>" +
                       "<p>Buna " + nameOfRecipient + ",</p>" +
                       "<p>In data de " + finalDate + " expira " + expiringObject + " la masina cu numarul " + registrationNumber + " </p>" +
                       "<p>O zi frumoasa!</p>" +
                       "<p>&nbsp;</p>" +
                       "<p>Acest mesaj a fost generat automat. Va rugam sa nu raspundeti.</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailOfRecipient);

            smtpClient.Send(mailMessage);
        }
    }
}
