using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;

namespace D03CBX_HFT_2023241.Logic {
    public class AlbumLogic : IAlbumLogic {

        IRepository<Album> repo;

        public AlbumLogic(IRepository<Album> repo) {
            this.repo = repo;
        }
        public void Create(Album item) {
            if (item.AlbumName.Length < 2) {
                throw new ArgumentException("Album name too short. At least 2 characters");
            }
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
        //public int AlbumCount() {
        //    return repo.ReadAll().Count();
        //}


        public IEnumerable<string> ListByYear(int year) {
            var list = repo.ReadAll();
            var filtered = list.Where(a => a.ReleaseYear == year)
                               .Select(h => h.AlbumName);

            if (filtered == null) {
                throw new NullReferenceException($"There are no albums published in {year}");
            }
            return filtered;
        }

        public IEnumerable<string> AveragePlaysAlbum() {
            var albums = repo.ReadAll();
            var result = albums.GroupBy(album => album.AlbumName)
                               .Select(group => new {
                                   AlbumName = group.Key,
                                   AveragePlays = group.SelectMany(album => album.Records)
                                                       .Average(record => record.Plays)
                               })
                               .Select(album => $"{album.AlbumName}: Average plays: {album.AveragePlays}");
            return result;
        }
    }
}
