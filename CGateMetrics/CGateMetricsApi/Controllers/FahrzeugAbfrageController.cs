using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using Microsoft.AspNetCore.Mvc;

namespace CGateMetricsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FahrzeugAbfrageController : ControllerBase
    {
        private readonly IFahrzeugAbfrageService _abfrageService;

        public FahrzeugAbfrageController(IFahrzeugAbfrageService abfrageService)
        {
            _abfrageService = abfrageService;
        }

        /// <summary>
        /// Get all bookings form drivers id number
        /// </summary>
        /// <param name="ausweisnummer">id number of driver</param>
        /// <returns>Bookings</returns>
        [HttpGet("Buchung/ByDriver/{ausweisnummer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Buchung>>> GetBuchungByDriver(string ausweisnummer)
        {
            try
            {
                var buchungen = await _abfrageService.GetBuchungByDriverId(ausweisnummer);
                return Ok(buchungen);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load Data. " + ex.Message);
            }

        }

        /// <summary>
        /// Get count of drivers between two timestamps
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="start">Starttime</param>
        /// <param name="end">Endtime</param>
        /// <returns>Count of drivers</returns>
        [HttpGet("CountBetween/{location}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> GetDriverCountByLocationWithTimeFilter(string location, [FromQuery] DateTime start, DateTime end)
        {
            try
            {
                var count = await _abfrageService.GetDriverCountByLocationWithinTimeFrame(location, start, end);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load Data. " + ex.Message);
            }

        }

        ///// <summary>
        ///// Get count of drivers for location
        ///// </summary>
        ///// <param name="location">Location</param>
        ///// <returns>Count of drivers</returns>
        //[HttpGet("Count/{location}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<int>> GetDriverCountByLocationAlltime(string location)
        //{
        //    try
        //    {
        //        var count = await _abfrageService.GetDriverCountByLocationAlltime(location);
        //        return Ok(count);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Could not load Data. " + ex.Message);
        //    }

        //}

        /// <summary>
        /// Get all vehicles where overloaded.
        /// </summary>
        /// <returns>List of vehicles</returns>
        [HttpGet("Overloaded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Fahrzeug>>> GetOverloadedLKWs()
        {
            try
            {
                var fahrzeuge = await _abfrageService.GetOverloadedLKWs();
                return Ok(fahrzeuge);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load Data. " + ex.Message);
            }

        }

        /// <summary>
        /// Get all vehicles where current on the location.
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>List of vehicles</returns>
        [HttpGet("CurrentOnLocation/{location}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Fahrzeug>>> GetCurrentLKWSOnLocation(string location)
        {
            try
            {
                var fahrzeuge = await _abfrageService.GetCurrentLKWSOnLocation(location);
                return Ok(fahrzeuge);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load Data. " + ex.Message);
            }

        }

        /// <summary>
        /// Get all drivers with incompleted dataset.
        /// </summary>
        /// <returns>List of drivers</returns>
        [HttpGet("Driver/Incomplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Fahrer>>> GetDriversWithIncompleteData()
        {
            try
            {
                var fahrer = await _abfrageService.GetDriversWithIncompleteData();
                return Ok(fahrer);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not load Data. " + ex.Message);
            }

        }
    }
}
