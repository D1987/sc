using Microsoft.EntityFrameworkCore;
using Server.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Dal.EntityConfigurations
{
    public class HostConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder) {}   
    }
}
