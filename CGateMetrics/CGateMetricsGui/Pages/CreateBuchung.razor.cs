using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using CGateMetricsGui.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;

namespace CGateMetricsGui.Pages
{
    public partial class CreateBuchung
    {

        [Inject]
        public CGateMetricsDbContext _context { get; set; }

        [Inject]
        NavigationManager Navi { get; set; }

        [Inject]
        IPageHistoryService PageHistory { get; set; }

        [Parameter]
        public int Id { get; set; } = 0;

        public List<string> AusweisIdList { get; set; }
        public List<string> FahrgestellnummerList { get; set; }
        public List<int> StandortIdList { get; set; }
        public List<string> StandortNameList { get; set; }

        Buchung buchung = new()
        {
            UhrzeitIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
            UhrzeitOut = null,
            AusweisId = "",
            Fahrgestellnummer = "",
            StandortId = 0,
            GewichtIn = 0,
            GewichtOut = 0,
            Gefahrgut = ""
        };

        string currentStandortName = "";

        protected async override Task OnParametersSetAsync()
        {
            if (Id != 0)
            {
                buchung = await _context.Buchungen.FindAsync(Id);
            }

            AusweisIdList = await _context.Fahrer.Select(x => x.AusweisId).ToListAsync();
            FahrgestellnummerList = await _context.Fahrzeuge.Select(x => x.Fahrgestellnummer).ToListAsync();
            StandortNameList = await _context.Standort.Select(x => x.Standortname).ToListAsync();
        }

        public async Task SubmitButtonPressed()
        {
            buchung.StandortId = _context.Standort.SingleOrDefault(x => x.Standortname == currentStandortName).Id;

            if (Id!=0)
            {
                _context.Update(buchung);
                await _context.SaveChangesAsync();
                //Navi.NavigateTo("/Buchungen");
                await PageHistory.Back(); 
            }
            else
            {
                await _context.Buchungen.AddAsync(buchung);
                buchung.StandortId = _context.Standort.Where(y => y.Id == buchung.StandortId).Select(x => x.Id).First();
                await _context.SaveChangesAsync();
                //Navi.NavigateTo("/Buchungen");
                await PageHistory.Back();
            }
        }

        public bool checkMaxWeight(int? gewicht)
        {
            if(gewicht > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkMinWeight(int? gewicht)
        {
            if (gewicht < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool compareTime(DateTime? timeIn, DateTime? timeOut)
        {
            if (timeOut <= timeIn)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}


