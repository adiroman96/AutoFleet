﻿using AutoFleet.Data;
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
        private readonly int INTERVAL = 2; //the time interval at which messages are sent

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
            DateTime correctDate = DateTime.Now.Date;

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (now.CompareTo(correctDate) == 0)
                {
                    List<Driver> drivers = dbContext.Drivers.ToList<Driver>();

                    EmailHelper.SendMail(drivers[0].Name, "as24ady@gmail.com", "RCA", "01/01/2021", "SB01AAA");
                }
            }

           
        }
    }
}
