using CGateMetricsData;
using CGateMetricsGui.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsGui.Pages
{
    public partial class Auswertungen2Page
    {
        [Inject] public CGateMetricsDbContext Context { get; set; }

        private List<CountLkwMonth> _countLkwMonthList = new();
        private List<WeightDataItem> _weightDataItemList = new();
        private List<BookingCountOfLocation> _bookingCountOfLocationList = new();
        private DateTime _startTime;
        private DateTime _endTime;

        protected override async Task OnInitializedAsync()
        {
            _startTime = DateTime.Now.AddDays(-30).Date;
            _endTime = DateTime.Now.Date;

            await LoadData();
        }

        private async Task LoadData()
        {
            _countLkwMonthList = await Context.Buchungen
                .Where(f => f.UhrzeitIn > _startTime && f.UhrzeitIn < _endTime)
                .GroupBy(f => f.UhrzeitIn.Date)
                .Select(f =>
                new CountLkwMonth
                {
                    Date = f.Key,
                    Count = f.Count()
                })
                .ToListAsync();


            _weightDataItemList = await Context.Buchungen
                 .Where(f => f.UhrzeitIn > _startTime && f.UhrzeitIn < _endTime)
                 .Select(f =>
                     new WeightDataItem
                     {
                         Time = f.UhrzeitIn.TimeOfDay,
                         Datetime = f.UhrzeitIn,
                         Weight = f.GewichtIn
                     })
                 .ToListAsync();

            _bookingCountOfLocationList = await Context.Buchungen
                .GroupBy(f => f.Standort)
                .Select(f =>
                    new BookingCountOfLocation
                    {
                        Location = f.Key.Standortname,
                        Count = f.Count()
                    })
                .ToListAsync();
        }
    }
}
