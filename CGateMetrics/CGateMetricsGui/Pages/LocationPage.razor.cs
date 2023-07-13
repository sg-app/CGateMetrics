using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;

namespace CGateMetricsGui.Pages
{
    public partial class LocationPage
    {
        [Inject] CGateMetricsDbContext Context { get; set; }
        [Inject] public DialogService DialogService { get; set; }
        [Inject] public ILogger<LocationPage> Logger { get; set; }

        private RadzenDataGrid<Standort> _grid;
        private List<Standort> _data;
        private bool _isLoading = true;
        private int _count;

        private async Task LoadData(LoadDataArgs args)
        {
            Logger.LogInformation($"Loading all required Data. Custom filter: {args.Filter}, Custom order: {args.OrderBy}, Custom paging: Skip {args.Skip.Value} Take {args.Top.Value}");
            _isLoading = true;
            var query = Context.Standort.AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                query = query.OrderBy(args.OrderBy);
            }

            _count = query.Count();

            _data = query.Skip(args.Skip.Value).Take(args.Top.Value).ToList();
            _isLoading = false;
        }

        private async Task EditButton(Standort item)
        {
            Logger.LogInformation($"Edit location with Id: {item.Id} and Name: {item.Standortname}");
            var result = await DialogService.OpenAsync<CreateEditLocation>("Location",
                new Dictionary<string, object>() { { "Standort", item } });

            if (result is bool? && result == true)
            {
                Logger.LogInformation($"Save location item wiht Id: {item.Id} new Name: {item.Standortname}");
                var toUpdate = await Context.Standort.FindAsync(item.Id);
                if (toUpdate != null)
                {
                    Context.Standort.Update(toUpdate);
                    await Context.SaveChangesAsync();
                }
            }

        }

        private async Task DeleteButton(Standort item)
        {
            Logger.LogInformation($"Request delete location with Id: {item.Id} and Name: {item.Standortname}");
            var result = await DialogService.Confirm("Soll Eintrag gelöscht werden?", "Löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });
            if (result != null && result == true)
            {
                Logger.LogInformation($"Delete location confirmed!");
                var toDelete = await Context.Standort.FindAsync(item.Id);
                if (toDelete != null)
                {
                    Context.Standort.Remove(toDelete);
                    await Context.SaveChangesAsync();
                    _data.Remove(item);
                    await _grid.Reload();
                }
            }
        }

        private async Task InsertButton()
        {
            Logger.LogInformation($"Insert new location.");
            Standort? standort = new();
            var result = await DialogService.OpenAsync<CreateEditLocation>("Location",
                new Dictionary<string, object>() { { "Standort", standort } });

            if (result is bool? && result == true)
            {
                await Context.Standort.AddAsync(standort);
                await Context.SaveChangesAsync();
                _data.Add(standort);
                Logger.LogInformation($"New location item wiht Id: {standort.Id} and Name: {standort.Standortname} are inserted.");
                await _grid.Reload();
            }
        }
    }
}
