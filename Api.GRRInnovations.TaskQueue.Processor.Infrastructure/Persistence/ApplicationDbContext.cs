using Api.GRRInnovations.TaskQueue.Processor.Domain.Entities;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Threading;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DefaultModelSetup<TaskModel>(modelBuilder);
            modelBuilder.Entity<TaskModel>().Property(m => m.Type).HasConversion(new EnumToStringConverter<ETaskType>()).HasMaxLength(100);
            modelBuilder.Entity<TaskModel>().Property(m => m.Status).HasConversion(new EnumToStringConverter<ETaskStatus>()).HasMaxLength(100);
            modelBuilder.Entity<TaskModel>().Property(m => m.Priority).HasConversion(new EnumToStringConverter<ETaskPriority>()).HasMaxLength(100);
        }

        public override int SaveChanges()
        {
            int result;
            try
            {
                AdjustChanges();
                result = base.SaveChanges();
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result;
            try
            {
                AdjustChanges();
                result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }

            return result;
        }

        public void RollBack()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        private void AdjustChanges()
        {
            var changes = ChangeTracker.Entries<BaseModel>().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added);

            foreach (var entry in changes)
            {
                entry.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }

        public void DefaultModelSetup<T>(ModelBuilder modelBuilder) where T : BaseModel
        {
            modelBuilder.Entity<T>().Property(m => m.CreatedAt).IsRequired();
            modelBuilder.Entity<T>().Property(m => m.UpdatedAt).IsRequired();

            modelBuilder.Entity<T>().HasKey(m => m.Uid);
            modelBuilder.Entity<T>().Property((m) => m.Uid).IsRequired().HasValueGenerator<GuidValueGenerator>();
        }
    }
}
