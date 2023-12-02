using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Infrastructure.EF.Config
{
    public class DbContextConfiguration
       : IEntityTypeConfiguration<User>,
        IEntityTypeConfiguration<Domain.Entities.Task>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }

        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Manager)
                .WithMany(m => m.Tasks)
                .HasForeignKey(t => t.ManagerId);
            builder.HasOne(t => t.Worker)
                .WithOne(w => w.AssignedTask)
                .HasForeignKey<Domain.Entities.Task>(t => t.WorkerId);
        }
    }
}
