using CGateMetricsData;
using CGateMetricsGui.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CGateMetricsGui.Components.Diagramms
{

    public partial class WeightSeries
    {
        [Inject] public CGateMetricsDbContext Context { get; set; }
        private List<WeightDataItem> _data = new();

        protected override async Task OnInitializedAsync()
        {
            _data = await Context.Buchungen
                 .Where(f => f.UhrzeitIn.Date == new DateTime(1996, 12, 6))
                 //.Where(f => f.UhrzeitIn.Date > new DateTime(1996, 12, 6) && f.UhrzeitIn.Date < new DateTime(1997, 5, 6))
                 .Select(f =>
                     new WeightDataItem
                     {
                         Time = f.UhrzeitIn.TimeOfDay,
                         Datetime = f.UhrzeitIn,
                         Weight = f.GewichtIn
                     })
                 .ToListAsync();

        }
        string FormatAsWeight(object value)
        {
            return ((double)value).ToString("N0");
        }

        
   
    }
}
