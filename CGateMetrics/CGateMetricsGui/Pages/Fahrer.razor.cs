using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace CGateMetricsGui.Pages
{
    public partial class Fahrer
    {

        private RadzenDataGrid<CGateMetricsData.Models.Fahrer> _fahrerGrid;

        [Inject] 
        public CGateMetricsDbContext _context { get; set; }
        
        List<CGateMetricsData.Models.Fahrer> _fahrer = new();

        protected async override Task OnInitializedAsync()
        {
             _fahrer = await _context.Fahrer.ToListAsync();
        }


        protected async Task EditButton(CGateMetricsData.Models.Fahrer item)
        {

        }


        protected async Task DeleteButton(CGateMetricsData.Models.Fahrer item)
        {
            bool? deleteConfirm = await DialogService.Confirm("Sind Sie sicher?", "Fahrer löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

            if (deleteConfirm == true) 
            { 
                _fahrer.Remove(item);

                var fahrer = await _context.Fahrer.FindAsync(item.AusweisId);
                _context.Fahrer.Remove(fahrer);
                await _context.SaveChangesAsync();
                await _fahrerGrid.Reload(); 

            }



        }

    }
}


