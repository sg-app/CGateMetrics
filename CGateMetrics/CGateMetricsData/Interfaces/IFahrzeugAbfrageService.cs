using CGateMetricsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Interfaces
{
    public interface IFahrzeugAbfrageService
    {
        Task<List<Buchung>> GetBuchungByDriverId(string ausweisnummer);
        Task<int> GetDriverCountByLocationWithinTimeFrame(string location, DateTime? startTimeFilter, DateTime? endTimeFilter); 
        Task<List<Fahrzeug>> GetOverloadedLKWs();
        Task<List<Fahrzeug>> GetCurrentLKWSOnLocation(string location);
        Task<List<Fahrer>> GetDriversWithIncompleteData();

    }
}
