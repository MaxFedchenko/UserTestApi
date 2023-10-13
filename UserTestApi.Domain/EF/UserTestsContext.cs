using Microsoft.EntityFrameworkCore;
using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.EF
{
    public class UserTestsContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<UserTestEntity> UserTests { get; set; }

        public UserTestsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>(u => 
                u.HasIndex(u => u.Name).IsUnique());
            builder.Entity<TestEntity>().OwnsMany(t => t.Questions, 
                tonb =>
                {
                    tonb.ToJson();
                    tonb.OwnsMany(q => q.Options);
                });
        }
    }
}
