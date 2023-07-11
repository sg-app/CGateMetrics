using CGateMetricsData;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace CGateMetricsGui.Pages
{
    public partial class Fahrer
    {

        [Inject] 
        public CGateMetricsDbContext _context { get; set; }
        
        List<CGateMetricsData.Models.Fahrer> _fahrer = new();

        protected async override Task OnInitializedAsync()
        {
             _fahrer = await _context.Fahrer.ToListAsync();
        }
    }
}


