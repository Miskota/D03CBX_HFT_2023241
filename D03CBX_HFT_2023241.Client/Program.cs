using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading;
using ConsoleTools;
using D03CBX_HFT_2023241.Models;
//using D03CBX_HFT_2023241.Repository;

namespace D03CBX_HFT_2023241.Client {
    internal class Program {

        static RestService rest;
        static void Main(string[] args) {
            
            // PingableEndpoint should be swagger (can leave it blank, it's the default value)
            rest = new RestService("http://localhost:59244/");
            // Note: This app opens http://localhost:59244/
            //       Instead of http://localhost:59244/swagger/index.html

            var writerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Writer"))
                .Add("Create", () => Create("Writer"))
                .Add("Delete", () => Delete("Writer"))
                .Add("Update", () => Update("Writer"))
                .Add("Exit", ConsoleMenu.Close);
            
            var albumSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Album"))
                .Add("Create", () => Create("Album"))
                .Add("Delete", () => Delete("Album"))
                .Add("Update", () => Update("Album"))
                .Add("Exit", ConsoleMenu.Close);

            var recordSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Record"))
                .Add("Create", () => Create("Record"))
                .Add("Delete", () => Delete("Record"))
                .Add("Update", () => Update("Record"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Writers with albums in a genre", () => WritersWithAlbumsInGenre())
                .Add("First and latest albums from a given writer", () => OldestLatestAlbums())
                .Add("List a given writer's albums", () => ListAlbums())
                .Add("Top 10 writers with the most albums", () => Top10AlbumCount())
                .Add("List every album from a given year", () => ListByYear())
                .Add("Average plays for each album", () => AveragePlaysAlbum())
                .Add("Statistics for each genre", () => GenreStatistics())
                .Add("List every record in a genre", () => ListByGenre())
                .Add("Most played records (Top 10)", () => Top10Plays())
                .Add("Records with the best ratings (Top 10)", () => Top10Rated())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Writers", () => writerSubMenu.Show())
                .Add("Albums", () => albumSubMenu.Show())
                .Add("Records", () => recordSubMenu.Show())
                .Add("Statistics", () => nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
        // NonCrud methods
        static void WritersWithAlbumsInGenre() {
            
            Console.WriteLine("Enter the Genre ");
            Console.WriteLine("Choices: Classic/Jazz/Country/Pop/Rock/Metal/Electro/Punk/Folk/Disco/Funk/Synth/HipHop  ");
            string genre = Console.ReadLine();
            try {
                var val = rest.Get<Writer>($"/NonCrud/WritersWithAlbumsInGenre/{genre}");
                Console.WriteLine($"The following writers have at least one album with the chosen genre");
                if (val == null) {
                    Console.WriteLine("None found");
                } 
                else {
                    foreach (var item in val) {
                        Console.WriteLine($"==============================\n" +
                                          $"\tName: {item.WriterName}\n" +
                                          $"\tID: {item.WriterID}\n" +
                                          $"\tAge: {item.Age}\n"
                                          );
                    }
                }
            }
            catch (ArgumentException) {
                Console.WriteLine($"Invalid Genre: {genre}");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }
        static void OldestLatestAlbums() {
            Console.WriteLine($"Enter WriterID: ");
            try {
                int id = int.Parse(Console.ReadLine());
                var albums = rest.Get<Album>($"/NonCrud/OldestLatestAlbums/{id}");
                Album first = (Album)albums.First();
                Album latest = (Album)albums.Last();
                Console.WriteLine($"Writer: {first.Writer.WriterName}\n" +
                                  $"First album: {first.AlbumName} | {first.ReleaseYear}\n" +
                                  $"Latest album: {latest.AlbumName} | {latest.ReleaseYear}\n");
            }
            catch (FormatException) {
                Console.WriteLine("Invalid ID format");
            }
            catch (NullReferenceException) {
                Console.WriteLine("No data found");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }
        static void ListAlbums() {
            Console.WriteLine("Enter WriterID: ");
            try {
                int id = int.Parse(Console.ReadLine());
                var albums = rest.Get<Album>($"/NonCrud/ListAlbums/{id}");
                var writerName = albums.First().Writer.WriterName;
                Console.WriteLine($"Albums from {writerName}");
                foreach (var item in albums) {
                    Console.WriteLine($"========================\n" +
                                      $"\tAlbumID: {item.AlbumID}\n" +
                                      $"\tName: {item.AlbumName}\n" +
                                      $"\tGenre: {item.Genre}\n" +
                                      $"\tReleased in {item.ReleaseYear}");
                }
            } 
            catch (FormatException) {
                Console.WriteLine("Invalid ID format");
            }
            catch (NullReferenceException) {
                Console.WriteLine("No data found");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }
        static void Top10AlbumCount() {
            Console.WriteLine("");
            var list = rest.Get<Writer>($"/NonCrud/Top10AlbumCount");
            int i = 1;
            foreach (var writer in list) {
                Console.WriteLine($"{i}: {writer.WriterName} : {writer.Albums.Count()}");
                i++;
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void ListByYear() {
            Console.WriteLine("Enter the year");
            try {
                int year = int.Parse(Console.ReadLine());
                var albums = rest.Get<string>($"/NonCrud/ListByYear/{year}");
                Console.WriteLine($"Albums from {year}: ");
                foreach (var album in albums) {
                    Console.WriteLine(album);
                }
            }
            catch (FormatException) {
                Console.WriteLine("Invalid ID format");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }
        static void AveragePlaysAlbum() {
            var stats = rest.Get<string>($"/NonCrud/AveragePlaysAlbum");
            foreach (var item in stats) {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void GenreStatistics() {
            var list = rest.Get<string>("/NonCrud/GenreStatistics");
            foreach (var item in list) {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void ListByGenre() {
            string genre = Console.ReadLine();
            var list = rest.Get<Record>($"/NonCrud/ListByGenre/{genre}");
            foreach (var item in list) {
                Console.WriteLine($"{item.RecordID} | {item.Title}");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void Top10Plays() {
            var list = rest.Get<Record>("/NonCrud/Top10Plays");
            int i = 1;
            Console.WriteLine("Most played songs");
            foreach (var item in list) {
                Console.WriteLine($"{i}: {item.Title} | {item.Plays}");
                i++;
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void Top10Rated() {
            var list = rest.Get<Record>("/NonCrud/Top10Rated");
            int i = 1;
            Console.WriteLine("Most rated songs");
            foreach (var item in list) {
                Console.WriteLine($"{i}: {item.Title} | {item.Rating}");
            }
            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void Create(string entity) {
            Console.WriteLine(entity + " create");
            
            // If the key auto increments, then we don't have to give it an id here.
            // Properties: RecordID, AlbumID, Title, Plays, Rating, Duration, Genre
            if (entity == "Record") {
                
                Console.WriteLine("Enter Title: ");
                string title = Console.ReadLine();

                Console.WriteLine("Enter AlbumID: ");
                int albumId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter number of plays: ");
                int plays = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter rating: ");
                double rating = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter song's length in seconds: ");
                int duration = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter genre: ");
                Genre genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine());

                var obj = new Record() {
                    Title = title,
                    AlbumID = albumId,
                    Plays = plays,
                    Rating = rating,
                    Duration = duration,
                    Genre = genre
                };
                rest.Post(obj, "record");
            }

            // Properties: AlbumID, WriterID, AlbumName, Genre, ReleaseYear
            else if (entity == "Album") {
                Console.WriteLine("Enter WriterID: ");
                int writerId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Name: ");
                string albumName = Console.ReadLine();

                Console.WriteLine("Enter Genre: ");
                Genre genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine());

                Console.WriteLine("Enter the year of release: ");
                int year = int.Parse(Console.ReadLine());

                var obj = new Album() {
                    WriterID = writerId,
                    AlbumName = albumName,
                    Genre = genre,
                    ReleaseYear = year
                };
                rest.Post(obj, "album");
            }

            // Properties: WriterID, YearOfBirth, WriterName
            else if (entity == "Writer") {
                Console.WriteLine("Enter the Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter year of birth: ");
                int year = int.Parse(Console.ReadLine());

                var obj = new Writer() {
                    WriterName = name,
                    YearOfBirth = year
                };
                rest.Post(obj, "writer");
            }
        }


        static void List(string entity) {
            Console.WriteLine(entity + " list");

            if (entity == "Record") {
                List<Record> records = rest.Get<Record>("record");
                foreach (var record in records) {
                    Console.WriteLine($"\tID: {record.RecordID} | Title: {record.Title} | Plays: {record.Plays} | Rating: {record.Rating}");
                }
            } 
            else if (entity == "Album") {
                List<Album> albums = rest.Get<Album>("album");
                foreach (var album in albums) {
                    Console.WriteLine($"\tID: {album.AlbumID} | Name: {album.AlbumName} | Genre: {album.Genre}");
                }
            } 
            else if (entity == "Writer") {
                List<Writer> writers = rest.Get<Writer>("writer");
                foreach (var writer in writers) {
                    Console.WriteLine($"\tID: {writer.WriterID} | Name: {writer.WriterName} | Age: {writer.Age}");
                }
            }
            else {
                // Won't happen with ConsoleMenu
                Console.WriteLine("Invalid Entity");
            }
            Console.WriteLine("\nPress any button to return...");
            Console.ReadKey();
        }




        static void Update(string entity) {
            Console.WriteLine(entity + " update");
            
            // Title might change for a song, but its number of plays and rating can change.
            // There's no reason to implement a method that changes every property. Just a few
            // Properties to update: Title, Plays, Rating
            if (entity == "Record") {
                Console.WriteLine("Enter RecordID: ");
                int id = int.Parse(Console.ReadLine());
                var record = rest.Get<Record>(id, "record");

                Console.WriteLine($"Enter new Title [Old: {record.Title}]: ");
                record.Title = Console.ReadLine();

                Console.WriteLine($"Enter new Playcount [Old: {record.Plays}]: ");
                record.Plays = int.Parse(Console.ReadLine());

                Console.WriteLine($"Enter new Rating [Old: {record.Rating}]: ");
                record.Rating = double.Parse(Console.ReadLine());
                rest.Put(record, "record");
            }


            // Properties to update: AlbumName, Genre
            else if (entity == "Album") {
                Console.WriteLine("Enter AlbumID: ");
                int id = int.Parse(Console.ReadLine());
                var album = rest.Get<Album>(id, "album");

                try {
                    Console.WriteLine($"Enter the new name for the album [Old: {album.AlbumName}]: ");
                    album.AlbumName = Console.ReadLine();

                    Console.WriteLine($"Enter the new genre for the album [Old: {album.Genre}]");
                    album.Genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine());
                } catch (FormatException) {
                    // If the user enters an invalid Genre
                    Console.WriteLine($"Couldn't parse the given string");
                }
                
            }
            // Properties to update: Name
            // Ran out of ideas. That's the only property that can change.
            // Like someone's date of birth is constant and cannot be changed.
            else if (entity == "Writer") {
                Console.WriteLine("Enter WriterID: ");
                int id = int.Parse(Console.ReadLine());
                var writer = rest.Get<Writer>(id, "writer");
   
            }
            Console.WriteLine("\nPress any button to return...");
            Console.ReadKey();
        }




        static void Delete(string entity) {
            Console.WriteLine(entity + " delete");

            Console.WriteLine("Enter the ID of the item to be deleted");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine($"You are about to delete a(n) {entity} with ID:{id}. Do you wish to proceed? Type stop to cancel: ");
            var choice = Console.ReadLine();
            if (choice == "stop") {
                return;
            }
            
            if (entity == "Record") {
                rest.Delete(id, "record");
            }
            else if (entity == "Album") {
                rest.Delete(id, "album");
            }
            else if (entity == "Writer") {
                rest.Delete(id, "writer");
            }
            Console.WriteLine("Entity deleted. Press any key to return...");
            Console.ReadKey();
        }
    }
}
