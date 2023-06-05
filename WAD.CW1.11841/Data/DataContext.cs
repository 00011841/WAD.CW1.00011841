using Microsoft.EntityFrameworkCore;
using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne<Teacher>(t => t.Teacher)
                .WithMany(s => s.Students)
                .HasForeignKey(t => t.TeacherId);
        }
    }
}
