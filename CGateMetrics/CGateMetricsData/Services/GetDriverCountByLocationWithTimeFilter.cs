using CGateMetricsData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Services
{
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {

        /// <summary>
        /// Returns a the count of all booking entries for a location within the defined timeframe
        /// </summary>
        /// <param name="location"></param>
        /// <param name="startTimeFilter">nullable</param>
        /// <param name="endTimeFilter">nullable</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> GetDriverCountByLocationWithinTimeFrame(string location, DateTime? startTimeFilter, DateTime? endTimeFilter)
        {
            int anzahlFahrer = 0;

            if (location == null)
                throw new ArgumentNullException($"{nameof(GetDriverCountByLocationWithinTimeFrame)}");

            if (startTimeFilter == null && endTimeFilter != null)
                anzahlFahrer = _context.Buchungen.Count(x => x.Standort.Standortname == location && x.UhrzeitOut <= endTimeFilter);

            if (startTimeFilter != null && endTimeFilter == null)
                anzahlFahrer = _context.Buchungen.Count(x => x.Standort.Standortname == location && x.UhrzeitIn >= startTimeFilter);

            if (startTimeFilter != null && endTimeFilter != null)
                anzahlFahrer = _context.Buchungen.Count(x => x.Standort.Standortname == location && x.UhrzeitIn >= startTimeFilter && x.UhrzeitOut <= endTimeFilter);

            if (startTimeFilter == null && endTimeFilter == null)
                anzahlFahrer = _context.Buchungen.Count(x => x.Standort.Standortname == location);

            return anzahlFahrer;

        }

    }
}
