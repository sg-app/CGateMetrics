using Radzen;
using CGateMetricsGui.Models;
using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsGui.Components.Diagramms
{
    public partial class OverloadPercent
    {
        bool showDataLabels = false;

        public int FahrzeugNormalLoad { get; set; } = 0;
        public int FahrzeugOverload { get; set; } = 0;

        public List<Overload> revenue { get; set; } = new List<Overload>();

        public List<Overload> revenue1 { get; set; } = new List<Overload>();
        public List<Overload> revenue2 { get; set; } = new List<Overload>();
        public List<Overload> revenue3 { get; set; } = new List<Overload>();


        [Inject]
        public CGateMetricsDbContext _context { get; set; }

        protected async override Task OnInitializedAsync()
        {
            revenue1 = await OverloadData(2018);
            revenue2 = await OverloadData(2019);
            revenue3 = await OverloadData(2020);
        }


        public async Task<List<Overload>> OverloadData(int year)
        {

            int normalLoad = await _context.Buchungen.Where(b => b.GewichtIn <= b.Fahrzeug.ZulGesamtGewicht && b.UhrzeitIn >= new DateTime(year, 01, 01) && b.UhrzeitIn <= new DateTime(year, 12, 31)).Include(i => i.Fahrzeug).CountAsync();
            int overLoad = await _context.Buchungen.Where(b => b.GewichtIn > b.Fahrzeug.ZulGesamtGewicht && b.UhrzeitIn >= new DateTime(year, 01, 01) && b.UhrzeitIn <= new DateTime(year, 12, 31)).Include(i => i.Fahrzeug).CountAsync();


            return new List<Overload> { new Overload
            {
                Load = "normal",
                Revenue = normalLoad,
            },
            new Overload
            {
                Load = "überladen",
                Revenue = overLoad,
            }
            };

        }



    }
}

