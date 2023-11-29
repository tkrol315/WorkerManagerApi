using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkerManager.Infrastructure.EF.Config
{
    public class TaskConfiguration : IEntityTypeConfiguration<WorkerManager.Infrastructure.EF.Models.Task>
    {
        public void Configure(EntityTypeBuilder<WorkerManager.Infrastructure.EF.Models.Task> builder)
        {
           builder.HasOne(t => t.Creator).WithMany(u => u.Tasks).HasForeignKey(t => t.CreatorId);
           builder.HasOne(t => t.User).WithOne(u => u.AssignedTask).HasForeignKey<WorkerManager.Infrastructure.EF.Models.Task>(t => t.UserId);
        }
    }
}
