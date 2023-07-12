using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace CGateMetricsGui.Components
{
    public partial class FahrerCreateAndEdit
    {

        [Inject]
        public DialogService DialogService { get; set; }

        public List<string> Anrede { get; set; }

        public bool DisableAusweisId { get; set; }

        [Parameter]
        public CGateMetricsData.Models.Fahrer Fahrer { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Anrede = new List<String>() { "Herr", "Frau", "Divers" };

            if(Fahrer.AusweisId != null)
            {
                DisableAusweisId = true;
            }
        }

        public async Task SubmitButtonPressed()
        {
            DialogService.Close(true);
        }
   

    }
}




