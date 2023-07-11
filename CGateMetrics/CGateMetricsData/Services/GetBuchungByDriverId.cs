using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {
        public async Task<List<Buchung>> GetBuchungByDriverId(string ausweisnummer)
        {

            return  await _context.Buchungen.Where(d => d.AusweisId == ausweisnummer).ToListAsync(); 



        }

    } 







}
