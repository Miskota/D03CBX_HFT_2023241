using System;
using System.Collections.Generic;
using ConsoleTools;
using D03CBX_HFT_2023241.Models;
//using D03CBX_HFT_2023241.Repository;

namespace D03CBX_HFT_2023241.Client {
    internal class Program {

        static RestService rest;
        static void Main(string[] args) {
            //var ctx = new MusicDbContext();

            rest = new RestService("http://localhost:59244/", "music"); 
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

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Writers", () => writerSubMenu.Show())
                .Add("Albums", () => albumSubMenu.Show())
                .Add("Records", () => recordSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        static void Create(string entity) {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity) {
            Console.WriteLine(entity + " list");
            Console.ReadLine();

            if (entity == "Record") {
                List<Record> records = rest.Get<Record>("record");
                foreach (var item in records) {
                    Console.WriteLine(item.Title);
                }
            }
        }
        static void Update(string entity) {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity) {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }
    }
}
