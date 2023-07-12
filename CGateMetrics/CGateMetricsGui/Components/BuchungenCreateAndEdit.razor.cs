using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace CGateMetricsGui.Components
{
    public partial class BuchungenCreateAndEdit
    {

        [Inject]
        public DialogService DialogService { get; set; }


        [Parameter]
        public CGateMetricsData.Models.Buchung Buchung { get; set; }

        protected async override Task OnInitializedAsync()
        {

        }


        public async Task SubmitButtonPressed()
        {

            DialogService.Close(true);

        }





        

    }
}




