using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Dal.EntityConfigurations
{
    public class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder
                .HasOne(c => c.Vm)
                .WithMany(m => m.Apps)
                .HasForeignKey(c => c.VmId);
        }
    }
}
