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
        public async Task<List<Buchung>> GetBuchungByDriverId(string ausweisnummer)
        {


            var buchung =  _context.Buchungen.FirstOrDefault(d => d.AusweisId == ausweisnummer);


            var buchungslist = new List<Buchung>
            {
                buchung
            };

            return buchungslist;



        }

    }







}
