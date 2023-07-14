using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsGui.Interfaces;
using CGateMetricsGui.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace CGateMetricsGui.Components
{
    public partial class FahrzeugePageCreatandEdit
    {


        [Inject]
        public DialogService DialogService { get; set; }

        public TooltipService TooltipService { get; set; }

        [Inject]
        IPageHistoryService PageHistory { get; set; }
        [Inject]
        public CGateMetricsDbContext _context { get; set; }

        [Parameter]
        public CGateMetricsData.Models.Fahrzeug Fahrzeug { get; set; }

        Fahrzeug fahrzeug = new()
        {
           
            Fahrgestellnummer = "",
            Hersteller = "",
            Kennzeichen = "",
            ZulGesamtGewicht = 0
           
        };


        CGateMetricsData.Models.Fahrzeug item = new();

        List<CGateMetricsData.Models.Fahrzeug> _fahrzeug = new();

        protected async override Task OnInitializedAsync()
        {

            _fahrzeug = await _context.Fahrzeuge.ToListAsync();

        }



        public async Task SubmitButtonPressed()
        {

            if (item.Fahrgestellnummer != null && item.Hersteller != null && item.Hersteller != null &&
                item.ZulGesamtGewicht != null)
            {
                _context.Update(fahrzeug);
                await _context.SaveChangesAsync();

                DialogService.Close(true);
            }

            else
            {


                var confirm = await DialogService.Alert("Sie haben fehlende eingaben", "");



                if (confirm == true)
                {

                    
                    DialogService.Close(false);

                }

                DialogService.Close(false);
            }


        }

        public async Task CloseTask()
        {

            DialogService.Close(false);

        }


        


        
    }
}
