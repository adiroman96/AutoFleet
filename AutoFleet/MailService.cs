using AutoFleet.Data;
using AutoFleet.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFleet.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFleet
{
    public class MailService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly int INTERVAL = 2; //the time interval at which messages are sent (hours)

        public MailService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(TaskRoutine, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }

        public Task TaskRoutine()
        {

            while (true)
            {
                //Check if there are any emails to send and send them
                sendEmailsIfNeeded();

                //Wait X hours till next execution, where X is the
                DateTime nextStop = DateTime.Now.AddMinutes(INTERVAL); // ToDo: change in hours
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }

        private void sendEmailsIfNeeded()
        {
            DateTime now = DateTime.Now.Date;

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                List<Insurance> insurances = dbContext.Insurances.AsQueryable<Insurance>().Where(x => x.ReminderDate.CompareTo(now) <= 0).ToList<Insurance>();

                insurances.ForEach(insurance =>
                {
                    Car car = dbContext.Cars.Find(insurance.CarId);
                    Driver driver = dbContext.Drivers.Find(car.DriverId);

                    EmailHelper.SendMail(driver.Name, driver.Email, insurance.TypeOfInsurance, insurance.ExpirationDate.Date.ToString("dd/MM/yyyy"), car.RegistrationNumber);
                });
            }           
        }
    }
}
