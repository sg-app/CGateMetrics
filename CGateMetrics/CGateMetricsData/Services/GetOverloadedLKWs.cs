using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {

        public async Task<List<Fahrzeug>> GetOverloadedLKWs()
        {
            List<Buchung> buchungen = await _context.Buchungen.Include(i => i.Fahrzeug).ToListAsync();
            return buchungen.Where(b => b.GewichtIn > b.Fahrzeug.ZulGesamtGewicht).Select(b => b.Fahrzeug).ToList();
        }
    }
}






