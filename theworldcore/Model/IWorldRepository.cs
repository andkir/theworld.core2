using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace theworldcore.Model
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip newTrip);

        Trip GetTripByName(string tripName);

        Task<bool> SaveChangesAsync();
        void AddStop(string tripName, Stop newStop, string username);
        IEnumerable<Trip> GetTripsByUsername(string username);

        Trip GetUserTripByName(string tripName, string username);
    }

    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext worldContext;

        public WorldRepository(WorldContext worldContext)
        {
            this.worldContext = worldContext;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            return worldContext.Trips.ToList();
        }

        public void AddTrip(Trip newTrip)
        {
            worldContext.Trips.Add(newTrip);
        }

        public Trip GetTripByName(string tripName)
        {
            return worldContext.Trips.Include(t=>t.Stops).FirstOrDefault(t => t.Name == tripName);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await (worldContext.SaveChangesAsync())>0;
        }

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);

            if (trip != null)
            {
                trip.Stops.Add(newStop);
                worldContext.Stops.Add(newStop);
            }
        }

        public IEnumerable<Trip> GetTripsByUsername(string username)
        {
            return worldContext.Trips
                .Include(t => t.Stops)
                .Where(t => t.UserName == username)
                .ToList();
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return worldContext.Trips.Include(t => t.Stops)
                .FirstOrDefault(t => t.Name == tripName && t.UserName == username);
        }
    }
}
