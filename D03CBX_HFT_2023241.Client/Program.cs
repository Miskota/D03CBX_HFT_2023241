using System;
using System.Collections.Generic;
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
                //.Add("Statistics (NonCrud)", ())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
        // NonCrud methods
        static void WritersWithAlbumsInGenre() {

        }
        static void OldestLatestAlbums() {

        }
        static void ListAlbums() {

        }
        static void Top10AlbumCount() {

        }

        static void ListByYear() {

        }
        static void AveragePlaysAlbum() {

        }

        static void GenreStatistics() {

        }
        static void ListByGenre() {

        }
        static void Top10Plays() {

        }
        static void Top10Rated() {

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
