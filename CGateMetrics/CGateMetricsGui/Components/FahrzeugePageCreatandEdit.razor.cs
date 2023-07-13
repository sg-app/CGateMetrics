using CGateMetricsData;
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




        [Parameter]
        public CGateMetricsData.Models.Fahrzeug Fahrzeug { get; set; }

        protected async override Task OnInitializedAsync()
        {
          
        }


        public async Task SubmitButtonPressed()
        {

            DialogService.Close(true);

        }



        public async Task CloseTask()
        {

            DialogService.Close(false);

        }


        


        
    }
}
