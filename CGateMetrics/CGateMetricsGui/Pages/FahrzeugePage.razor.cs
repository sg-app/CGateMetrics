using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace CGateMetricsGui.Pages


{
    public partial class FahrzeugePage
    {

        private RadzenDataGrid<CGateMetricsData.Models.Fahrzeug> _fahrzeugGrid;

        [Inject] public CGateMetricsDbContext _context { get; set; }

        List<CGateMetricsData.Models.Fahrzeug> _fahrzeug = new();

        protected async override Task OnInitializedAsync()
        {
            _fahrzeug = await _context.Fahrzeuge.ToListAsync();
        }


        protected async Task CreateNewFahrzeug()
        {
            CGateMetricsData.Models.Fahrzeug item = new();

            bool? saveChanges = await DialogService.OpenAsync<FahrzeugePageCreatandEdit>("Create and Edit",
                new Dictionary<string, object>() { { "Fahrzeug", item } },
                new DialogOptions() { Width = "700px", Height = "612px", Resizable = true, Draggable = true, });

            if (item.Fahrgestellnummer != null | item.Hersteller != null | item.Kennzeichen != null)
            {




                if (saveChanges.HasValue)
                {
                    CGateMetricsData.Models.Fahrzeug fahrzeug = new CGateMetricsData.Models.Fahrzeug();
                    fahrzeug.Fahrgestellnummer = item.Fahrgestellnummer;
                    fahrzeug.Kennzeichen = item.Kennzeichen;
                    fahrzeug.Hersteller = item.Hersteller;
                    fahrzeug.ZulGesamtGewicht = item.ZulGesamtGewicht;
                    _context.Add(fahrzeug);
                    _fahrzeug.Add(fahrzeug);
                    await _context.SaveChangesAsync();
                }

            }
        }

        protected async Task EditButton(CGateMetricsData.Models.Fahrzeug item)
        {
            bool? saveChanges = await DialogService.OpenAsync<FahrzeugePageCreatandEdit>("Create and Edit",
                new Dictionary<string, object>() { { "Fahrzeug", item } },
                new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true, ShowClose = true });


        if  (item != null)
        {
            
        



            if (saveChanges.HasValue)
            
            { 
               var fahrzeug = await _context.Fahrzeuge.FindAsync(item.Fahrgestellnummer);
                fahrzeug.Fahrgestellnummer = item.Fahrgestellnummer;
                fahrzeug.Kennzeichen = item.Kennzeichen;
                fahrzeug.Hersteller = item.Hersteller;
                fahrzeug.ZulGesamtGewicht = item.ZulGesamtGewicht;
                _context.Update(fahrzeug);
                await _context.SaveChangesAsync();
            }

        }



        }


        protected async Task DeleteButton(CGateMetricsData.Models.Fahrzeug item)
        {
            bool? deleteConfirm = await DialogService.Confirm("Sind Sie sicher?", "Fahrzeug löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

            if (deleteConfirm == true)
            {
                _fahrzeug.Remove(item);

                var fahrzeug = await _context.Fahrzeuge.FindAsync(item.Fahrgestellnummer);
                _context.Fahrzeuge.Remove(fahrzeug);
                await _context.SaveChangesAsync();
                await _fahrzeugGrid.Reload();

            }
        }



    }
    }

