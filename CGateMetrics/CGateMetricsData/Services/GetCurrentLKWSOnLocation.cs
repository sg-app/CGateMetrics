using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {


        public async Task<List<Fahrzeug>> GetCurrentLKWSOnLocation(string location)
        {

            var currentLKWS =  _context.Buchungen.FirstOrDefault(f => f.Standort == location);

            
            var lkwsonlocation = new List<Fahrzeug>
            {
                currentLKWS.Fahrzeug
        };

            return lkwsonlocation;





        }



    }
}
