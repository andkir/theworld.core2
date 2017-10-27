using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using theworldcore.Model;

namespace theworldcore.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripsController: Controller
    {
        private readonly IWorldRepository repository;

        public TripsController(IWorldRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           await Task.Delay(1000);
           return Ok(repository.GetTripsByUsername(User.Identity.Name));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Trip newTrip)
        {
            if (ModelState.IsValid)
            {
                newTrip.UserName = User.Identity.Name;
                repository.AddTrip(newTrip);
                await repository.SaveChangesAsync();

                await Task.Delay(1000);
                return Created($"api/trips/{newTrip.Id}", newTrip);
            }

            return BadRequest("Failed to save trip");
        }
    }
}
