using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {


        public async Task<List<Fahrzeug>> GetCurrentLKWSOnLocation(string location)
        {
            var standort = await _context.Standort.FirstOrDefaultAsync(f => f.Standortname == location);
            var buchung = _context.Buchungen.Where(i => i.StandortId == standort.Id && i.UhrzeitOut == null).Include(f => f.Fahrzeug);
            return await buchung.Select(b => b.Fahrzeug).ToListAsync();
        }


    }
}
