using System;
using System.Collections.Generic;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Data;

public partial class Task6DbContext : DbContext
{
    public Task6DbContext()
    {
    }

    public Task6DbContext(DbContextOptions<Task6DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Element> Elements { get; set; }

    public virtual DbSet<Elementtype> Elementtypes { get; set; }

    public virtual DbSet<Presentation> Presentations { get; set; }

    public virtual DbSet<Sharedpresentationslide> Sharedpresentationslides { get; set; }

    public virtual DbSet<Sharedslideelement> Sharedslideelements { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Element>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("elements_pkey");

            entity.ToTable("elements");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Type).WithMany(p => p.Elements)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("elements_typeid_fkey");
        });

        modelBuilder.Entity<Elementtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("elementtypes_pkey");

            entity.ToTable("elementtypes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Presentation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("presentations_pkey");

            entity.ToTable("presentations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(32)
                .HasColumnName("author");
            entity.Property(e => e.Title)
                .HasMaxLength(32)
                .HasDefaultValueSql("'Title'::character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Sharedpresentationslide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sharedpresentationslides_pkey");

            entity.ToTable("sharedpresentationslides");

            entity.HasIndex(e => new { e.Presentationid, e.Slideid }, "sharedpresentationslides_presentationid_slideid_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Presentationid).HasColumnName("presentationid");
            entity.Property(e => e.Slideid).HasColumnName("slideid");

            entity.HasOne(d => d.Presentation).WithMany(p => p.Sharedpresentationslides)
                .HasForeignKey(d => d.Presentationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sharedpresentationslides_presentationid_fkey");

            entity.HasOne(d => d.Slide).WithMany(p => p.Sharedpresentationslides)
                .HasForeignKey(d => d.Slideid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sharedpresentationslides_slideid_fkey");
        });

        modelBuilder.Entity<Sharedslideelement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sharedslideelements_pkey");

            entity.ToTable("sharedslideelements");

            entity.HasIndex(e => new { e.Slideid, e.Elementid }, "sharedslideelements_slideid_elementid_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ElementContent)
                .HasDefaultValueSql("''::text")
                .HasColumnName("element_content");
            entity.Property(e => e.ElementHeight)
                .HasDefaultValue(30)
                .HasColumnName("element_height");
            entity.Property(e => e.ElementWidth)
                .HasDefaultValue(100)
                .HasColumnName("element_width");
            entity.Property(e => e.ElementX)
                .HasDefaultValue(0)
                .HasColumnName("element_x");
            entity.Property(e => e.ElementY)
                .HasDefaultValue(0)
                .HasColumnName("element_y");
            entity.Property(e => e.Elementid).HasColumnName("elementid");
            entity.Property(e => e.Slideid).HasColumnName("slideid");

            entity.HasOne(d => d.Element).WithMany(p => p.Sharedslideelements)
                .HasForeignKey(d => d.Elementid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sharedslideelements_elementid_fkey");

            entity.HasOne(d => d.Slide).WithMany(p => p.Sharedslideelements)
                .HasForeignKey(d => d.Slideid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sharedslideelements_slideid_fkey");
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("slides_pkey");

            entity.ToTable("slides");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .HasMaxLength(32)
                .HasDefaultValueSql("'white'::character varying")
                .HasColumnName("background");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
