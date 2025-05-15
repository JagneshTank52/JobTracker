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

        // User
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


        // User Role
        modelBuilder.Entity<UserRole>().ToTable("UserRole");
        modelBuilder.Entity<UserRole>()
            .HasKey(h => h.Id);

        modelBuilder.Entity<UserRole>()
        .Property(b => b.Id);

        modelBuilder.Entity<UserRole>()
        .Property(b => b.Name)
        .HasMaxLength(250)
        .IsRequired();

        // Job Status
        modelBuilder.Entity<JobStatus>(entity =>
        {
            entity.ToTable("Jobstatus");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.Id);

            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(2500);
        });

        //Jobs
        modelBuilder.Entity<Job>(entity =>
          {
              entity.ToTable("Jobs");

              entity.HasKey(e => e.Id);

              entity.Property(e => e.JobTitle)
                  .IsRequired()
                  .HasColumnType("varchar(250)")
                  .HasMaxLength(250);

              entity.Property(e => e.JobDescription)
                  .HasColumnType("text");

              entity.Property(e => e.CompanyName)
                  .IsRequired()
                  .HasColumnType("varchar(250)")
                  .HasMaxLength(250);

              entity.Property(e => e.JobLocation)
                  .IsRequired()
                  .HasColumnType("varchar(250)")
                  .HasMaxLength(250);

              entity.Property(e => e.NoOfApplicant)
                  .HasColumnType("integer");

              entity.Property(e => e.IsDeleted)
                  .IsRequired()
                  .HasColumnType("boolean")
                  .HasDefaultValue(false);

              entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("now()");

              entity.Property(e => e.ModifiedAt)
                  .HasColumnType("timestamp without time zone");
          });

        // JobUserMapping
        modelBuilder.Entity<JobUserMapping>(entity =>
         {
             entity.ToTable("JobUserMapping");

             entity.HasKey(e => e.Id);

             entity.HasOne(d => d.Job)
                .WithMany(p => p.JobUserMappings)
                .HasForeignKey(d => d.JobId)
                .IsRequired();

             entity.HasOne(d => d.User)
                .WithMany(p => p.JobUserMappings)
                .HasForeignKey(d => d.UserId)
                .IsRequired();

             entity.HasOne(d => d.JobStatus)
                .WithMany(p => p.JobUserMappings)
                .HasForeignKey(d => d.StatusId)
                .IsRequired();

             entity.Property(e => e.UserResume)
                 .HasColumnType("text")
                 .IsRequired();

             entity.Property(e => e.Comment)
                 .IsRequired()
                 .HasColumnType("text");

             entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasColumnType("boolean")
                .HasDefaultValue(false);

             entity.Property(e => e.CreatedAt)
                   .IsRequired()
                   .HasColumnType("timestamp without time zone")
                   .HasDefaultValueSql("now()");

             entity.Property(e => e.ModifiedAt)
                 .HasColumnType("timestamp without time zone");
         });

    }

}
