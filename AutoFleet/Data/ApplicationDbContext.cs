﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoFleet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoFleet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<ITP> Itps { get; set; }
        public DbSet<CASCO> Cascos { get; set; }
        public DbSet<Rca> Rcas { get; set; }
        public DbSet<Rovinieta> Rovinietas { get; set; }

    }
}
