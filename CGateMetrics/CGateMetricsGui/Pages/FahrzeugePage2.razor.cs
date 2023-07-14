using CGateMetricsData.Models;
using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using CGateMetricsGui.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CGateMetricsGui.Pages
{
    public partial class FahrzeugePage2
    {
        [Inject] CGateMetricsDbContext Context { get; set; }
        [Inject] public DialogService DialogService { get; set; }
        [Inject] public ILogger<FahrzeugePage2> Logger { get; set; }

        private RadzenDataGrid<Fahrzeug> _grid;
        private List<Fahrzeug> _data;
        private bool _isLoading = true;
        private int _count;


        private async Task LoadData(LoadDataArgs args)
        {
            Logger.LogInformation($"Loading all required Data. Custom filter: {args.Filter}, Custom order: {args.OrderBy}, Custom paging: Skip {args.Skip.Value} Take {args.Top.Value}");
            _isLoading = true;
            var query = Context.Fahrzeuge.AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                query = query.OrderBy(args.OrderBy);
            }

            _count = query.Count();

            _data = await query.Skip(args.Skip.Value).Take(args.Top.Value).ToListAsync();
            _isLoading = false;
        }

        private async Task EditButton(Fahrzeug item)
        {
            Logger.LogInformation($"Edit vehicle with Number: {item.Fahrgestellnummer} and Kennzeichen: {item.Kennzeichen}");
            var result = await DialogService.OpenAsync<CreateEditFahrzeug>("Fahrzeug",
                new Dictionary<string, object>() { { "Fahrzeug", item } });

            if (result is bool? && result == true)
            {
                Logger.LogInformation($"Save vehilce item with number: {item.Fahrgestellnummer}");
                var toUpdate = await Context.Fahrzeuge.FindAsync(item.Fahrgestellnummer);
                if (toUpdate != null)
                {
                    Context.Fahrzeuge.Update(toUpdate);
                    await Context.SaveChangesAsync();
                }
            }

        }

        private async Task DeleteButton(Fahrzeug item)
        {
            Logger.LogInformation($"Request delete vehicle with nummber: {item.Fahrgestellnummer} and Kennzeichen: {item.Kennzeichen}");
            var result = await DialogService.Confirm("Soll Eintrag gelöscht werden?", "Löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (result != null && result == true)
            {
                Logger.LogInformation($"Delete vehicle confirmed!");
                var toDelete = await Context.Fahrzeuge.FindAsync(item.Fahrgestellnummer);
                if (toDelete != null)
                {
                    Context.Fahrzeuge.Remove(toDelete);
                    await Context.SaveChangesAsync();
                    _data.Remove(item);
                    await _grid.Reload();
                }
            }
        }

        private async Task InsertButton()
        {
            Logger.LogInformation($"Insert new vehicle.");
            Fahrzeug? fahrzeug = new();
            var result = await DialogService.OpenAsync<CreateEditFahrzeug>("Fahrzeug",
                new Dictionary<string, object>() { { "Fahrzeug", fahrzeug } });

            if (result is bool? && result == true)
            {
                await Context.Fahrzeuge.AddAsync(fahrzeug);
                await Context.SaveChangesAsync();
                _data.Add(fahrzeug);
                Logger.LogInformation($"New vehicle item wiht Number: {fahrzeug.Fahrgestellnummer} and Kennzeichen: {fahrzeug.Kennzeichen} are inserted.");
                await _grid.Reload();
            }
        }
    }
}
