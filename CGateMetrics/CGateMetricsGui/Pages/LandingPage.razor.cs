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

        private string _currentLocation;

        private List<CurrentVehiclesIn> _currentVehiclesIn;

        protected override async Task OnInitializedAsync()
        {
            _locations = await Context.Buchungen
                .GroupBy(f=>f.Standort)
                .Select(f=>f.Key)
                .OrderBy(f=>f)
                .ToListAsync();

            if (_locations.Count > 0)
            {
                await LocationChangedHandler(_locations[0]);
            }
        }

        private async Task LoadData()
        {
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
