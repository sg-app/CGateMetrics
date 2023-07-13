using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

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

        Buchung buchung = new() {
            UhrzeitIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
            UhrzeitOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
            AusweisId = "",
            Fahrgestellnummer = "",
            Standort = "",
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
            await _context.SaveChangesAsync();
        }

        //protected async Task CreateBuchung(CGateMetricsData.Models.Buchung item)
        //{
        //        var buchungToAdd = new Buchung()
        //        {
        //            UhrzeitIn = item.UhrzeitIn,
        //            UhrzeitOut = item.UhrzeitOut,
        //            AusweisId = item.AusweisId,
        //            Fahrgestellnummer = item.Fahrgestellnummer,
        //            Standort = item.Standort,
        //            GewichtIn = item.GewichtIn,
        //            GewichtOut = item.GewichtOut,
        //            Gefahrgut = item.Gefahrgut
        //        };

        //        await _context.AddAsync(buchungToAdd);
        //        await _context.SaveChangesAsync();
        //}
    }
}


