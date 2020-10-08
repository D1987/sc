using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Entities.Models;

namespace Server.Dal.EntityConfigurations
{
    class VMConfiguration : IEntityTypeConfiguration<VM>
    {
        public void Configure(EntityTypeBuilder<VM> builder)
        {
            builder
                .HasMany(m => m.Apps)
                .WithOne(c => c.Vm)
                .HasForeignKey(c => c.VmId);
        }
    }
}
