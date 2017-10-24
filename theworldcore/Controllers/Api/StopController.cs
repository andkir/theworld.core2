using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using theworldcore.Model;
using theworldcore.Services;

namespace theworldcore.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController: Controller
    {
        private readonly IWorldRepository repository;
        private readonly GeoCoordsService geoCoordsService;

        public StopController(IWorldRepository repository, GeoCoordsService geoCoordsService)
        {
            this.repository = repository;
            this.geoCoordsService = geoCoordsService;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {
            var trip = repository.GetTripByName(tripName);

            return Ok(trip.Stops.OrderBy(s=>s.Order).ToList());
        }

        [HttpPost]  
        public async Task<IActionResult> Post(string tripName, [FromBody] Stop newStop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await geoCoordsService.GetCoordsAsync(newStop.Name);
                    if (result.Success)
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        repository.AddStop(tripName, newStop, User.Identity.Name);
                        if (await repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{newStop.Id}", newStop);
                        }
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {

               Console.WriteLine($"Ouch!{ex}");
            }

            return BadRequest("Failed to save Stop");
        }
    }
}
