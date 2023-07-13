using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CGateMetricsGui.Pages
{
    public partial class LandingPage
    {
        [Inject] public CGateMetricsDbContext Context { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        
        private int _currentIn;
        private List<string> _locations;
        private bool _isBusy = false;

        private string _currentLocation;

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
                _locations = await Context.Buchungen
                    .GroupBy(f => f.Standort)
                    .Select(f => f.Key)
                    .OrderBy(f => f)
                    .ToListAsync();

                if (_locations.Count > 0)
                {
                    await LocationChangedHandler(_locations[0]);
                }
                _isBusy = false;
                await InvokeAsync(StateHasChanged);
            }
        }
        private async Task LoadData()
        {
            _isBusy = true;
            _currentIn = await Context.Buchungen
                .Where(f => f.Standort == _currentLocation && f.UhrzeitOut == null)
                .CountAsync();

            _currentVehiclesIn = await Context.Buchungen
                .Where(f => f.Standort == _currentLocation && f.UhrzeitOut == null)
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

        private async Task LocationChangedHandler(object location)
        {
            _currentLocation = (string)location;
            await LoadData();
        }

        private void NewBookingHandler()
        {
            NavigationManager.NavigateTo("/CreateBuchung");
        }
    }
}
