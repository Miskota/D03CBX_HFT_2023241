using D03CBX_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace D03CBX_HFT_2023241.Repository {
    public class MusicDBContext : DbContext {
        public DbSet<Record> Records { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Album> Albums { get; set; }

        public MusicDBContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Music.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conn);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Records)
                .WithOne(r => r.Album)
                .HasForeignKey(r => r.AlbumID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.Writer)
                .WithMany(w => w.Albums)
                .HasForeignKey(a => a.WriterID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Album)
                .WithMany(a => a.Records)
                .HasForeignKey(r => r.AlbumID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Writer>()
                .HasMany(w => w.Albums)
                .WithOne(a => a.Writer)
                .HasForeignKey(a => a.WriterID)
                .OnDelete(DeleteBehavior.Cascade);

            // Sample Writers
            modelBuilder.Entity<Writer>().HasData(
                new Writer() {
                        WriterID = 1,
                        WriterName = "John Lennon",
                        YearOfBirth = 1940
                },
                new Writer() {
                        WriterID = 2,
                        WriterName = "Daft Punk",
                        YearOfBirth = -1 // Will throw an exception, Daft Punk has 2 members with varying dates
                });

            // Sample Albums
            modelBuilder.Entity<Album>().HasData(
                new Album() {
                    AlbumID = 1,
                    AlbumName = "Abbey Road",
                    Genre = Genre.Rock,
                    ReleaseYear = 1969,
                    WriterID = 1 // John Lennon foreign key
                },
                new Album() {
                    AlbumID = 2,
                    AlbumName = "Random Access Memories",
                    Genre = Genre.Electro,
                    ReleaseYear = 2013,
                    WriterID = 2 // Daft Punk
                });

            // Sample records
            modelBuilder.Entity<Record>().HasData(
                new Record() {
                    RecordID = 1,
                    Title = "Come Together",
                    AlbumID = 1,
                    Plays = 10000,
                    Rating = 4.5,
                    Duration = 180,
                    Genre = Genre.Rock
                },
                new Record() {
                    RecordID = 2,
                    Title = "Get Lucky",
                    AlbumID = 2,
                    Plays = 1000000,
                    Rating = 4.0,
                    Duration = 240,
                    Genre = Genre.Electro
                });
        }
    }
}
