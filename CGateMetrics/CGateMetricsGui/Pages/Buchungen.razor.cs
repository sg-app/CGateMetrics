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
        
        List<CGateMetricsData.Models.Buchung> _buchungen = new();

        protected async override Task OnInitializedAsync()
        {
             _buchungen = await _context.Buchungen.ToListAsync();
        }


        protected async Task EditButton(CGateMetricsData.Models.Buchung item)
        {
            bool saveChanges = await DialogService.OpenAsync<BuchungenCreateAndEdit>("Create and Edit",
                       new Dictionary<string, object>() { { "Buchung", item } },
                       new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if(saveChanges)
            {
                var buchung = await _context.Buchungen.FindAsync(item.BuchungsId);
                buchung.UhrzeitIn = item.UhrzeitIn;
                buchung.UhrzeitOut = item.UhrzeitOut;
                buchung.AusweisId = item.AusweisId;
                buchung.Fahrgestellnummer = item.Fahrgestellnummer;
                buchung.Standort = item.Standort;
                buchung.GewichtIn = item.GewichtIn;
                buchung.GewichtOut = item.GewichtOut;
                buchung.Gefahrgut = item.Gefahrgut;
                _context.Update(buchung);
                await _context.SaveChangesAsync();
            }
        }

        protected async Task PostButton(CGateMetricsData.Models.Buchung item)
        {
            bool saveChanges = await DialogService.OpenAsync<BuchungenCreateAndEdit>("Add new",
                       new Dictionary<string, object>() { { "Buchung", item } },
                       new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });

            if (saveChanges)
            {
                var buchungToAdd = new Buchung()
                {
                    UhrzeitIn = item.UhrzeitIn,
                    UhrzeitOut = item.UhrzeitOut,
                    AusweisId = item.AusweisId,
                    Fahrgestellnummer = item.Fahrgestellnummer,
                    Standort = item.Standort,
                    GewichtIn = item.GewichtIn,
                    GewichtOut = item.GewichtOut,
                    Gefahrgut = item.Gefahrgut
                };

                await _context.AddAsync(buchungToAdd);
                await _context.SaveChangesAsync();
            }
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


