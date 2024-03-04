using Microsoft.EntityFrameworkCore;


namespace Alasson.Models
{
    public class AppDbContext : DbContext
    { //ORM
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.FullName);
            builder.Entity<Employee>().Property(p => p.FullName).IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.Email).IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.Charge).IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.Salary).IsRequired().HasMaxLength(30);

        }

    }
}
