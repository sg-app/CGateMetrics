using CGateMetricsData;
using CGateMetricsGui.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace CGateMetricsGui.Pages
{
    public partial class Fahrer
    {

        private RadzenDataGrid<CGateMetricsData.Models.Fahrer> _fahrerGrid;

        [Inject]
        public CGateMetricsDbContext _context { get; set; }

        List<CGateMetricsData.Models.Fahrer> _fahrer = new();

        public bool IsLoading { get; set; }

        public List<string> Anrede { get; set; }


        CGateMetricsData.Models.Fahrer fahrerToInsert;
        CGateMetricsData.Models.Fahrer fahrerToUpdate;

        bool DisableAusweisId { get; set; }

        bool DisableUpdateGrid { get; set; }


        string checkUniqueId { get; set; }


        protected async override Task OnInitializedAsync()
        {
             _fahrer = await _context.Fahrer.ToListAsync();
            Anrede = new List<String>() { "Herr", "Frau", "Divers" };
        }

        bool ValidateNewId(string id)
        {

            var checkUniqueId = _fahrer.Where(f => f.AusweisId == id).Count();
            if (checkUniqueId == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //protected async Task CreateNewFahrer()
        //{
        //    CGateMetricsData.Models.Fahrer item = new();
        //    bool? saveChanges = await DialogService.OpenAsync<FahrerCreateAndEdit>("Create and Edit",
        //               new Dictionary<string, object>() { { "Fahrer", item } },
        //               new DialogOptions() { Width = "700px", Height = "612px", Resizable = true, Draggable = true, });

        //    if (saveChanges != null)
        //    {
        //        CGateMetricsData.Models.Fahrer fahrer = new CGateMetricsData.Models.Fahrer();
        //        fahrer.AusweisId = item.AusweisId;
        //        fahrer.Vorname = item.Vorname;
        //        fahrer.Nachname = item.Nachname;
        //        fahrer.Geburtsort = item.Geburtsort;
        //        fahrer.Geburtstag = item.Geburtstag;
        //        fahrer.Anrede = item.Anrede;
        //        fahrer.Telefon = item.Telefon;
        //        fahrer.Firma = item.Firma;
        //        _context.Add(fahrer);
        //        _fahrer.Add(fahrer);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //protected async Task EditButton(CGateMetricsData.Models.Fahrer item)
        //{

        //    CGateMetricsData.Models.Fahrer _editFahrer = new CGateMetricsData.Models.Fahrer
        //    {
        //        Vorname = item.Vorname,
        //        Nachname = item.Nachname,
        //        Geburtsort = item.Geburtsort,
        //        Geburtstag = item.Geburtstag,
        //        Anrede = item.Anrede,
        //        Telefon = item.Telefon,
        //        Firma = item.Firma,
        //        AusweisId = item.AusweisId

        //    };


        //    bool? saveChanges = await DialogService.OpenAsync<FahrerCreateAndEdit>("Create and Edit",
        //               new Dictionary<string, object>() { { "Fahrer", _editFahrer } },
        //               new DialogOptions() { Width = "700px", Height = "612px", Resizable = true, Draggable = true,  });

        //    if(saveChanges != null)
        //    {


        //        var fahrer = await _context.Fahrer.FindAsync(item.AusweisId);
        //        fahrer.Vorname = _editFahrer.Vorname;
        //        fahrer.Nachname = _editFahrer.Nachname;
        //        fahrer.Geburtsort = _editFahrer.Geburtsort;
        //        fahrer.Geburtstag = _editFahrer.Geburtstag;
        //        fahrer.Anrede = _editFahrer.Anrede;
        //        fahrer.Telefon = _editFahrer.Telefon;
        //        fahrer.Firma = _editFahrer.Firma;
        //        _context.Update(fahrer);
        //        await _context.SaveChangesAsync();
        //    }
        //}



        //protected async Task DeleteButton(CGateMetricsData.Models.Fahrer item)
        //{
        //    bool? deleteConfirm = await DialogService.Confirm("Sind Sie sicher?", "Fahrer löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

        //    if (deleteConfirm == true) 
        //    { 
        //        _fahrer.Remove(item);

        //        var fahrer = await _context.Fahrer.FindAsync(item.AusweisId);
        //        _context.Fahrer.Remove(fahrer);
        //        await _context.SaveChangesAsync();
        //        await _fahrerGrid.Reload(); 
        //    }
        //}





        void Reset()
        {
            fahrerToInsert = null;
            fahrerToUpdate = null;
        }



        async Task EditRow(CGateMetricsData.Models.Fahrer fahrer)
        {
            DisableAusweisId = true;
            fahrerToUpdate = fahrer;
            await _fahrerGrid.EditRow(fahrer);
        }

        private async Task OnUpdateRow(CGateMetricsData.Models.Fahrer fahrer)
        {
            if (fahrer == fahrerToInsert)
            {
                fahrerToInsert = null;
            }

            fahrerToUpdate = null;

            _context.Update(fahrer);
            await _context.SaveChangesAsync();

        }

        async Task SaveRow(CGateMetricsData.Models.Fahrer fahrer)
        {

              await _fahrerGrid.UpdateRow(fahrer);
              //await _fahrerGrid.Reload();
        }

        void CancelEdit(CGateMetricsData.Models.Fahrer fahrer)
        {
            if (fahrer == fahrerToInsert)
            {
                fahrerToInsert = null;
            }

            fahrerToUpdate = null;

            _fahrerGrid.CancelEditRow(fahrer);

            var fahrerEntry = _context.Entry(fahrer);
            if (fahrerEntry.State == EntityState.Modified)
            {
                fahrerEntry.CurrentValues.SetValues(fahrerEntry.OriginalValues);
                fahrerEntry.State = EntityState.Unchanged;
            }
        }

        async Task DeleteRow(CGateMetricsData.Models.Fahrer fahrer)
        {
            if (fahrer == fahrerToInsert)
            {
                fahrerToInsert = null;
            }

            if (fahrer == fahrerToUpdate)
            {
                fahrerToUpdate = null;
            }

            if (_fahrer.Contains(fahrer))
            {
               
                bool? deleteConfirm = await DialogService.Confirm("Sind Sie sicher?", "Fahrer löschen", new ConfirmOptions() { OkButtonText = "Ja", CancelButtonText = "Nein" });

                if (deleteConfirm == true)
                {

                    var deletefahrer = await _context.Fahrer.FindAsync(fahrer.AusweisId);
                    _context.Fahrer.Remove(deletefahrer);
                    await _context.SaveChangesAsync();
                    await _fahrerGrid.Reload();

                    _fahrer.Remove(fahrer);
                }
                await _context.SaveChangesAsync();

                await _fahrerGrid.Reload();
            }
            else
            {
                _fahrerGrid.CancelEditRow(fahrer);
                await _fahrerGrid.Reload();
            }
        }

        async Task InsertRow()
        {
            DisableAusweisId = false;
            fahrerToInsert = new CGateMetricsData.Models.Fahrer();
            await _fahrerGrid.InsertRow(fahrerToInsert);

        }

        
        async Task OnCreateRow(CGateMetricsData.Models.Fahrer fahrer)
        {
            _context.Add(fahrer);
            await _context.SaveChangesAsync();
            fahrerToInsert = null;


        }


    }
}


