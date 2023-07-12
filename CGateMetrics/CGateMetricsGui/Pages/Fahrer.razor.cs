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


        protected async Task CreateNewFahrer()
        {
            CGateMetricsData.Models.Fahrer item = new();
            bool? saveChanges = await DialogService.OpenAsync<FahrerCreateAndEdit>("Create and Edit",
                       new Dictionary<string, object>() { { "Fahrer", item } },
                       new DialogOptions() { Width = "700px", Height = "612px", Resizable = true, Draggable = true, });

            if (saveChanges != null)
            {
                CGateMetricsData.Models.Fahrer fahrer = new CGateMetricsData.Models.Fahrer();
                fahrer.AusweisId = item.AusweisId;
                fahrer.Vorname = item.Vorname;
                fahrer.Nachname = item.Nachname;
                fahrer.Geburtsort = item.Geburtsort;
                fahrer.Geburtstag = item.Geburtstag;
                fahrer.Anrede = item.Anrede;
                fahrer.Telefon = item.Telefon;
                fahrer.Firma = item.Firma;
                _context.Add(fahrer);
                _fahrer.Add(fahrer);
                await _context.SaveChangesAsync();
            }
        }

        protected async Task EditButton(CGateMetricsData.Models.Fahrer item)
        {

            CGateMetricsData.Models.Fahrer _editFahrer = new CGateMetricsData.Models.Fahrer
            {
                Vorname = item.Vorname,
                Nachname = item.Nachname,
                Geburtsort = item.Geburtsort,
                Geburtstag = item.Geburtstag,
                Anrede = item.Anrede,
                Telefon = item.Telefon,
                Firma = item.Firma,
                AusweisId = item.AusweisId

            };


            bool? saveChanges = await DialogService.OpenAsync<FahrerCreateAndEdit>("Create and Edit",
                       new Dictionary<string, object>() { { "Fahrer", _editFahrer } },
                       new DialogOptions() { Width = "700px", Height = "612px", Resizable = true, Draggable = true,  });

            if(saveChanges != null)
            {
                

                var fahrer = await _context.Fahrer.FindAsync(item.AusweisId);
                fahrer.Vorname = _editFahrer.Vorname;
                fahrer.Nachname = _editFahrer.Nachname;
                fahrer.Geburtsort = _editFahrer.Geburtsort;
                fahrer.Geburtstag = _editFahrer.Geburtstag;
                fahrer.Anrede = _editFahrer.Anrede;
                fahrer.Telefon = _editFahrer.Telefon;
                fahrer.Firma = _editFahrer.Firma;
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


