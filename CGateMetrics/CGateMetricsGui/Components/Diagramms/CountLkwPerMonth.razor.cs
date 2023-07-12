using CGateMetricsData;
using CGateMetricsGui.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsGui.Components.Diagramms
{
    public partial class CountLkwPerMonth
    {
        [Inject] public CGateMetricsDbContext Context { get; set; }

        private List<CountLkwMonth> _data = new List<CountLkwMonth>();
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
            _data = await Context.Buchungen
                .Where(f => f.UhrzeitIn > _startTime && f.UhrzeitIn < _endTime)
                .GroupBy(f => f.UhrzeitIn.Date)
                .Select(f =>
                new CountLkwMonth
                {
                    Date = f.Key,
                    Count = f.Count()
                })
                .ToListAsync();
        }

    }
}
