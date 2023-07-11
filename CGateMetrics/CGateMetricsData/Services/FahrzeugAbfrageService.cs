using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {
        CGateMetricsDbContext _context;
        public FahrzeugAbfrageService(CGateMetricsDbContext context) { 
            _context = context;      
        }

        public Task<List<Buchung>> GetBuchungByDriverId(string ausweisnummer)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fahrzeug>> GetCurrentLKWSOnLocation(string location)
        {
            throw new NotImplementedException();
        }


    }
}
