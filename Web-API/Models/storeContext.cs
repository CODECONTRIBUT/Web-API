﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Web_API.Models;

public partial class storeContext : DbContext
{
    public storeContext()
    {
    }

    public storeContext(DbContextOptions<storeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Parentplatformofproduct> Parentplatformofproducts { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Screenshot> Screenshots { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Trailer> Trailers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("genres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GamesCount)
                .HasDefaultValueSql("'0'")
                .HasColumnName("games_count");
            entity.Property(e => e.ImageBackground)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("image_background");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<Parentplatformofproduct>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ParentPlatformId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("parentplatformofproduct");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.ParentPlatformId).HasColumnName("Parent_Platform_id");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("platforms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundImage)
                .HasColumnType("text")
                .HasColumnName("background_image");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MetaCritic).HasColumnName("meta_critic");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RatingTop).HasColumnName("rating_top");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TrailerId).HasColumnName("trailer_id");
        });

        modelBuilder.Entity<Screenshot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("screenshots");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDatetime)
                .HasColumnType("datetime")
                .HasColumnName("created_datetime");
            entity.Property(e => e.Image)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Screenshots)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_id");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Domain)
                .HasMaxLength(300)
                .HasColumnName("domain");
            entity.Property(e => e.GamesCount).HasColumnName("games_count");
            entity.Property(e => e.ImageBackground)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("image_background");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<Trailer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("trailers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Max)
                .HasColumnType("text")
                .HasColumnName("max");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Preview)
                .HasColumnType("text")
                .HasColumnName("preview");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e._480)
                .HasColumnType("text")
                .HasColumnName("480");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}