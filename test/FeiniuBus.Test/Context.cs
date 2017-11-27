using FeiniuBus.Test.Model;
using Microsoft.EntityFrameworkCore;

namespace FeiniuBus.Test
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<TestingDto> TestingDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestingDto>(entity =>
            {
                entity.ToTable("TestingDto");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<TestingDto.Extra>(entity =>
            {
                entity.ToTable("Extra");
                entity.HasKey(x => x.Guest);
            });
        }
    }
}