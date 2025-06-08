using System;
using System.Collections.Generic;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=qy5k--zhr8a98L;Database=Task6Db;Port=5432");
    }

}