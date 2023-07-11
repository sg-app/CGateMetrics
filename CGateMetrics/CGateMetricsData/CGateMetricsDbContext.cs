﻿using CGateMetricsData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData
{
    public class CGateMetricsDbContext : DbContext
    {
        public virtual DbSet<Fahrer> Fahrer { get; set; }
        public virtual DbSet<Fahrzeug> Fahrzeuge { get; set; }
        public virtual DbSet<Buchung> Buchungen { get; set; }

        public CGateMetricsDbContext()
        {
            
        }

        public CGateMetricsDbContext(DbContextOptions<CGateMetricsDbContext> options) : base (options)
        {
            
        }

    }
}
