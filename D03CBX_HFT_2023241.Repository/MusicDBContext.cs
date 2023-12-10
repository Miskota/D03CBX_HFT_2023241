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
                //optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conn);
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Records)
                .WithOne(r => r.Album)
                .HasForeignKey(r => r.AlbumID)
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
                    YearOfBirth = -1 // Daft Punk has 2 members with varying dates
                },
                new Writer() {
                    WriterID = 3,
                    WriterName = "Bob Dylan",
                    YearOfBirth = 1941
                },
                new Writer() {
                    WriterID = 4,
                    WriterName = "Michael Jackson",
                    YearOfBirth = 1958
                },
                new Writer() {
                    WriterID = 5,
                    WriterName = "David Bowie",
                    YearOfBirth = 1947
                },
                new Writer() {
                    WriterID = 6,
                    WriterName = "Aretha Franklin",
                    YearOfBirth = 1942
                },
                new Writer() {
                    WriterID = 7,
                    WriterName = "Elton John",
                    YearOfBirth = 1947
                },
                new Writer() {
                    WriterID = 8,
                    WriterName = "Prince",
                    YearOfBirth = 1958
                },
                new Writer() {
                    WriterID = 9,
                    WriterName = "Stevie Wonder",
                    YearOfBirth = 1950
                },
                new Writer() {
                    WriterID = 10,
                    WriterName = "Janis Joplin",
                    YearOfBirth = 1943
                }
                );

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
                },
                new Album() {
                    AlbumID = 3,
                    AlbumName = "Highway 61 Revisited",
                    Genre = Genre.Folk,
                    ReleaseYear = 1965,
                    WriterID = 3 // Bob Dylan
                },
                new Album() {
                    AlbumID = 4,
                    AlbumName = "Thriller",
                    Genre = Genre.Pop,
                    ReleaseYear = 1982,
                    WriterID = 4 // Michael Jackson
                },
                new Album() {
                    AlbumID = 5,
                    AlbumName = "The Rise and Fall of Ziggy Stardust",
                    Genre = Genre.Rock,
                    ReleaseYear = 1972,
                    WriterID = 5 // David Bowie
                },
                new Album() {
                    AlbumID = 6,
                    AlbumName = "I Never Loved a Man the Way I Love You",
                    Genre = Genre.Pop,
                    ReleaseYear = 1967,
                    WriterID = 6 // Aretha Franklin
                },
                new Album() {
                    AlbumID = 7,
                    AlbumName = "Goodbye Yellow Brick Road",
                    Genre = Genre.Pop,
                    ReleaseYear = 1973,
                    WriterID = 7 // Elton John
                },
                new Album() {
                    AlbumID = 8,
                    AlbumName = "Purple Rain",
                    Genre = Genre.Pop,
                    ReleaseYear = 1984,
                    WriterID = 8 // Prince
                },
                new Album() {
                    AlbumID = 9,
                    AlbumName = "Songs in the Key of Life",
                    Genre = Genre.Funk,
                    ReleaseYear = 1976,
                    WriterID = 9 // Stevie Wonder
                },
                new Album() {
                    AlbumID = 10,
                    AlbumName = "Pearl",
                    Genre = Genre.Rock,
                    ReleaseYear = 1971,
                    WriterID = 10 // Janis Joplin
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
                },
                new Record() {
                    RecordID = 3,
                    Title = "Blowin' in the Wind",
                    AlbumID = 3,
                    Plays = 5000,
                    Rating = 4.8,
                    Duration = 200,
                    Genre = Genre.Folk
                },
                new Record() {
                    RecordID = 4,
                    Title = "Billie Jean",
                    AlbumID = 4,
                    Plays = 1500000,
                    Rating = 4.7,
                    Duration = 300,
                    Genre = Genre.Pop
                },
                new Record() {
                    RecordID = 5,
                    Title = "Like a Rolling Stone",
                    AlbumID = 3,
                    Plays = 8000,
                    Rating = 4.6,
                    Duration = 220,
                    Genre = Genre.Folk
                },
                new Record() {
                    RecordID = 6,
                    Title = "Beat It",
                    AlbumID = 4,
                    Plays = 1200000,
                    Rating = 4.9,
                    Duration = 250,
                    Genre = Genre.Pop
                },
                new Record() {
                    RecordID = 7,
                    Title = "Space Oddity",
                    AlbumID = 5,
                    Plays = 12000,
                    Rating = 4.7,
                    Duration = 230,
                    Genre = Genre.Rock
                },
                new Record() {
                    RecordID = 8,
                    Title = "Respect",
                    AlbumID = 6,
                    Plays = 8000,
                    Rating = 4.9,
                    Duration = 180,
                    Genre = Genre.Pop
                },
                new Record() {
                    RecordID = 9,
                    Title = "Rocket Man",
                    AlbumID = 7,
                    Plays = 15000,
                    Rating = 4.6,
                    Duration = 200,
                    Genre = Genre.Pop
                },
                new Record() {
                    RecordID = 10,
                    Title = "When Doves Cry",
                    AlbumID = 8,
                    Plays = 1000000,
                    Rating = 4.8,
                    Duration = 220,
                    Genre = Genre.Pop
                },
                new Record() {
                    RecordID = 11,
                    Title = "Sir Duke",
                    AlbumID = 9,
                    Plays = 9000,
                    Rating = 4.5,
                    Duration = 210,
                    Genre = Genre.Funk
                },
                new Record() {
                    RecordID = 12,
                    Title = "Me and Bobby McGee",
                    AlbumID = 10,
                    Plays = 6000,
                    Rating = 4.6,
                    Duration = 190,
                    Genre = Genre.Rock
                }
                );
        }
    }
}
