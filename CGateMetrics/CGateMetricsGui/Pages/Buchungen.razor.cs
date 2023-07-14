using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;


namespace CGateMetricsGui.Pages
{
    public partial class Buchungen
    {

        private RadzenDataGrid<CGateMetricsData.Models.Buchung> _buchungenGrid;

        [Inject] 
        public CGateMetricsDbContext _context { get; set; }
        [Inject] public ILogger<CreateBuchung> Logger { get; set; }

        [Inject]
        public NavigationManager Navi { get; set; }

        List<CGateMetricsData.Models.Buchung> _buchungen;
        private bool _isLoading = true;
        private int _count;

        private async Task LoadData(LoadDataArgs args)
        {
            Logger.LogInformation($"Loading all required Data. Custom filter: {args.Filter}, Custom order: {args.OrderBy}, Custom paging: Skip {args.Skip.Value} Take {args.Top.Value}");
            _isLoading = true;
            var query = _context.Buchungen.AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                query = query.OrderBy(args.OrderBy);
            }

            _count = query.Count();

            _buchungen = await query.Skip(args.Skip.Value).Take(args.Top.Value).ToListAsync();
            _isLoading = false;
        }

        protected async Task EditButton(CGateMetricsData.Models.Buchung item)
        {
            Navi.NavigateTo($"/CreateBuchung/{item.BuchungsId}");
        }


        protected async Task CreateButton()
        {
            Navi.NavigateTo($"/CreateBuchung/");
        }

        protected async Task DeleteButton(CGateMetricsData.Models.Buchung item)
        {
            bool? deleteConfirm = await DialogService.Confirm("Sind Sie sicher?", "Buchung löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

            if (deleteConfirm == true) 
            { 
                _buchungen.Remove(item);

                var buchung = await _context.Buchungen.FindAsync(item.AusweisId);
                _context.Buchungen.Remove(buchung);
                await _context.SaveChangesAsync();
                await _buchungenGrid.Reload(); 

            }
        }

    }
}


