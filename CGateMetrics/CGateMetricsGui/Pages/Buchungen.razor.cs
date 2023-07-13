using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace CGateMetricsGui.Pages
{
    public partial class Buchungen
    {

        private RadzenDataGrid<CGateMetricsData.Models.Buchung> _buchungenGrid;

        [Inject] 
        public CGateMetricsDbContext _context { get; set; }

        public Uri inputUri { get; set; }

        [Inject]
        public NavigationManager Navi { get; set; }

        List<CGateMetricsData.Models.Buchung> _buchungen = new();

        protected async override Task OnInitializedAsync()
        {
             _buchungen = await _context.Buchungen.ToListAsync();
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


