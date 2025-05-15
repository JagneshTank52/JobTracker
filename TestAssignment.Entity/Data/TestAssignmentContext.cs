using Microsoft.EntityFrameworkCore;
using TestAssignment.Entity.Models;

namespace TestAssignment.Entity.Data;

public class TestAssignmentContext : DbContext
{
    public TestAssignmentContext(DbContextOptions<TestAssignmentContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> userRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<User>()
            .Property(b => b.Id)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.FirstName)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.LastName)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.UserName)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.Email)
            .IsUnicode()
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.Password)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.HasPassword)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.IsDeleted)
            .HasDefaultValueSql("true")
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(b => b.ModifiedAt)
            .HasColumnType("timestamp without time zone");

        modelBuilder.Entity<User>()
            .Property(b => b.UserRoleId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasOne(b => b.UserRole)
            .WithMany(p => p.Users)
            .HasForeignKey(f => f.UserRoleId)
            .IsRequired();


        modelBuilder.Entity<UserRole>().ToTable("UserRole");
        modelBuilder.Entity<UserRole>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<UserRole>()
        .Property(b => b.Id);

        modelBuilder.Entity<UserRole>()
        .Property(b => b.Name)
        .HasMaxLength(250)
        .IsRequired();
    }

}
