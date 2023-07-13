using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;

namespace CGateMetricsGui.Pages
{
    public partial class CreateBuchung
    {

        [Inject] 
        public CGateMetricsDbContext _context { get; set; }

        [Inject]
        NavigationManager Navi { get; set; }

        [Parameter]
        public int Id { get; set; } = 0;

        public List<string> AusweisIdList { get; set; }
        public List<string> FahrgestellnummerList { get; set; }

        public List<int> StandortIdList { get; set; }

        Buchung buchung = new() {
            UhrzeitIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
            UhrzeitOut = null,
            AusweisId = "",
            Fahrgestellnummer = "",
            StandortId = 0,
            GewichtIn = 0,
            GewichtOut = 0,
            Gefahrgut = ""
        };

        

        protected async override Task OnParametersSetAsync()
        {
            if (Id!=0)
            {
                buchung = await _context.Buchungen.FindAsync(Id);
            }

            AusweisIdList = await _context.Fahrer.Select(x => x.AusweisId).ToListAsync();
            FahrgestellnummerList = await _context.Fahrzeuge.Select(x => x.Fahrgestellnummer).ToListAsync();
            StandortIdList = await _context.Standort.Select(x => x.Id).ToListAsync();
        }

        public async Task SubmitButtonPressed()
        {

                _context.Update(buchung);
                await _context.SaveChangesAsync();
                Navi.NavigateTo("/Buchungen"); 

        }

        public async Task CreateButtonPressed()
        {
            await _context.Buchungen.AddAsync(buchung);
            buchung.Standort = _context.Standort.Where(y => y.Id == buchung.StandortId).Select(x => x.Standortname).First();
            await _context.SaveChangesAsync();
            Navi.NavigateTo("/Buchungen");
        }

    }
}


