using System;
using System.Collections.Generic;
using System.Linq;
using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;

namespace D03CBX_HFT_2023241.Logic {
    public class AlbumLogic {

        IRepository<Album> repo;

        public AlbumLogic(IRepository<Album> repo)
        {
            this.repo = repo;
        }
        public void Create(Album item) {
            repo.Create(item);
        }

        public void Delete(int id) {
            if (repo.Read(id) == null) {
                throw new ArgumentException($"No Album with ID:{id} was found");
            }
            repo.Delete(id);
        }

        public Album Read(int id) {
            var album = repo.Read(id);
            if (album == null) {
                throw new ArgumentException($"No Album with ID:{id} was found");
            }
            return album;
        }

        public IQueryable<Album> ReadAll() {
            return repo.ReadAll();
        }

        public void Update(Album item) {
            repo.Update(item);
        }


        // Non-CRUD
        // List albums from a given year
        public int AlbumCount() {
            return repo.ReadAll().Count();
        }

        
        public List<string> ListByYear(int year) {
            var list = repo.ReadAll();
            var filtered = list.Where(a => a.ReleaseYear == year)
                               .Select(h => h.AlbumName)
                               .ToList();
            if (filtered == null) {
                throw new NullReferenceException($"There are no albums published in {year}");
            }
            return filtered;
        }

        // Group by year, list albums
    }
}
