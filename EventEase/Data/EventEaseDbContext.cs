using System;
using System.Collections.Generic;
using EventEase.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Data;

public partial class EventEaseDbContext : DbContext
{
    public EventEaseDbContext()
    {
    }

    public EventEaseDbContext(DbContextOptions <EventEaseDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<BookingView> BookingViews { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD2B51BAE1");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Booking__EventID__3C69FB99");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Booking__VenueID__3D5E1FD2");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8709092EA68");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5D241D319EB");

            entity.ToTable("Venue");

            entity.HasIndex(e => e.Location, "UQ__Venue__E55D3B104C6EE3E7").IsUnique();

            entity.Property(e => e.VenueId).HasColumnName("VenueID");
            entity.Property(e => e.Capacity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VenueName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BookingView>().HasNoKey().ToView("BookingView");

    

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
