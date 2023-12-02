using Microsoft.EntityFrameworkCore;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Infrastructure.EF.Contexts
{
    public class WorkerManagerDbContext : DbContext
    {
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public WorkerManagerDbContext(DbContextOptions<WorkerManagerDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
