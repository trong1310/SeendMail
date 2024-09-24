using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Seenmail.Databases.SeenMail;

public partial class SeedMailContext : DbContext
{
    public SeedMailContext()
    {
    }

    public SeedMailContext(DbContextOptions<SeedMailContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Users> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasColumnType("enum('Nam','Nữ','Khác')");
            entity.Property(e => e.PassWord).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.Status).HasColumnType("enum('Active','Locked')");
        });

        OnModelCreatingPartial(modelBuilder);


    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
