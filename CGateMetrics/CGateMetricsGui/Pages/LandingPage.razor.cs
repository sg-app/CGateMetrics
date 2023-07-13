using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;

namespace CGateMetricsGui.Pages
{
    public partial class LandingPage
    {
        [Inject] public CGateMetricsDbContext Context { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ILogger<LandingPage> Logger { get; set; }
        
        private int _currentIn;
        private List<Standort> _locations;
        private bool _isBusy = false;

        private int _currentLocationId;

        private List<CurrentVehiclesIn> _currentVehiclesIn;

        protected override Task OnInitializedAsync()
        {
            _isBusy = true;
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                Logger.LogDebug($"Start with loading all Locations.");

                _locations = await Context.Standort.ToListAsync();

                if(_locations.Count > 0)
                {
                    Logger.LogInformation($"Locations loaded. Select first one.");
                    await LocationChangedHandler(_locations[0].Id);
                }
        
                _isBusy = false;
                await InvokeAsync(StateHasChanged);
            }
        }
        private async Task LoadData()
        {
            Logger.LogInformation($"Load all required data from DbContext.");

            _isBusy = true;
            _currentIn = await Context.Buchungen
                .Where(f => f.StandortId == _currentLocationId && f.UhrzeitOut == null)
                .CountAsync();

            _currentVehiclesIn = await Context.Buchungen
                .Where(f => f.StandortId == _currentLocationId && f.UhrzeitOut == null)
                .Select(s =>
                new CurrentVehiclesIn
                {
                    BookingId = s.BuchungsId,
                    Kennzeichen = s.Fahrzeug.Kennzeichen,
                    Fahrer = $"{s.Fahrer.Anrede} {s.Fahrer.Vorname} {s.Fahrer.Nachname}",
                    Firma = s.Fahrer.Firma
                })
                .ToListAsync();
            _isBusy = false;
        }

        private async Task LocationChangedHandler(object locationId)
        {
            Logger.LogDebug($"Location changed to Id: {locationId}");
            _currentLocationId = (int)locationId;
            await LoadData();
        }

        private void NewBookingHandler()
        {
            Logger.LogInformation($"Navigate to new booking page.");
            NavigationManager.NavigateTo("/CreateBuchung");
        }
    }
}
