using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace theworldcore.Model
{
    public class WorldContext: IdentityDbContext<WorldUser>
    {
        private readonly IConfigurationRoot config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            this.config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }

    public class WorldUser : IdentityUser
    {
        public string FirstTrip { get; set; }
    }
}
