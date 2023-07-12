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

        public List<Overload> revenue2018 { get; set; } = new List<Overload>();
        public List<Overload> revenue2019 { get; set; } = new List<Overload>();
        public List<Overload> revenue2020 { get; set; } = new List<Overload>();


        [Inject]
        public CGateMetricsDbContext _context { get; set; }



        protected async override Task OnInitializedAsync()
        {
            FahrzeugNormalLoad = await _context.Buchungen.Where(b => b.GewichtIn <= b.Fahrzeug.ZulGesamtGewicht && b.UhrzeitIn >= new DateTime(2018,01,01) && b.UhrzeitIn <= new DateTime(2019,12,31)).Include(i => i.Fahrzeug).CountAsync();
            FahrzeugOverload = await _context.Buchungen.Where(b => b.GewichtIn > b.Fahrzeug.ZulGesamtGewicht && b.UhrzeitIn >= new DateTime(2018, 01, 01) && b.UhrzeitIn <= new DateTime(2019, 12, 31)).Include(i => i.Fahrzeug).CountAsync();




            revenue.Add(new Overload
            {
                Load = "normal",
                Revenue = FahrzeugNormalLoad
            });
            revenue.Add(new Overload
            {
                Load = "überladen",
                Revenue = FahrzeugOverload
            });


            revenue2018 = await OverloadData(2018);
            revenue2019 = await OverloadData(2019);
            revenue2020 = await OverloadData(2020);
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

