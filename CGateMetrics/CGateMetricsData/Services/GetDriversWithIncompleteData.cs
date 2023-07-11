using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Services
{   
    public partial class FahrzeugAbfrageService : IFahrzeugAbfrageService
    {
        public async Task<List<Fahrer>> GetDriversWithIncompleteData()
        {
            return await _context.Fahrer.Where(f => f.Anrede == null || f.Telefon == null || f.Geburtsort == null || f.Geburtstag == null).ToListAsync();
        }

    }
}
