using Microsoft.EntityFrameworkCore;

namespace FeiniuBus.Test
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Model.TestingDto> TestingDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.TestingDto>(entity =>
            {
                entity.ToTable("TestingDto");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Model.TestingDto.Extra>(entity =>
            {
                entity.ToTable("Extra");
                entity.HasKey(x => x.Guest);
            });
        }
    }
}
