using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Dal.EntityConfigurations;
using Server.Entities.Models;
using Server.Entities.Security;

namespace Server.Dal
{
    public class ServerContext : IdentityDbContext<User>
    {
        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
           // Database.EnsureCreated();
        }

        public DbSet<Host> Hosts { get; set; }
        public DbSet<VM> VMs { get; set; }
        public DbSet<App> Apps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfigurationsFromAssembly(typeof(ServerContext).Assembly);

            builder
                .ApplyConfiguration(new AppConfiguration())
                .ApplyConfiguration(new HostConfiguration())
                .ApplyConfiguration(new VMConfiguration());
        }
    }
}
