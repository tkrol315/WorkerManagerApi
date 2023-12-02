using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkerManager.Infrastructure.EF.Config
{
    public class TaskConfiguration : IEntityTypeConfiguration<Models.Task>
    {
        public void Configure(EntityTypeBuilder<Models.Task> builder)
        {
           builder.HasOne(t => t.Manager).WithMany(m => m.Tasks).HasForeignKey(t => t.ManagerId);
           builder.HasOne(t => t.Worker).WithOne(w => w.AssignedTask).HasForeignKey<Models.Task>(t => t.WorkerId);
        }
    }
}
