using CGateMetricsData;
using CGateMetricsGui.Components;
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
            bool saveChanges = await DialogService.OpenAsync<FahrerCreateAndEdit>("Create and Edit",
                       new Dictionary<string, object>() { { "Fahrer", item } },
                       new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if(saveChanges)
            {
                var fahrer = await _context.Fahrer.FindAsync(item.AusweisId);
                fahrer.Vorname = item.Vorname;
                fahrer.Nachname = item.Nachname;
                fahrer.Geburtsort = item.Geburtsort;
                fahrer.Geburtstag = item.Geburtstag;
                fahrer.Anrede = item.Anrede;
                fahrer.Telefon = item.Telefon;
                fahrer.Firma = item.Firma;
                _context.Update(fahrer);
                await _context.SaveChangesAsync();
            }
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


