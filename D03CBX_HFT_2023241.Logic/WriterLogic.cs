using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Logic {
    public class WriterLogic : IWriterLogic {
        IRepository<Writer> repo;

        public WriterLogic(IRepository<Writer> repo) {
            this.repo = repo;
        }
        public void Create(Writer item) {
            repo.Create(item);
        }

        public void Delete(int id) {
            if (repo.Read(id) == null) {
                throw new ArgumentException($"No Writer with ID:{id} was found");
            }
            repo.Delete(id);
        }

        public Writer Read(int id) {
            var album = repo.Read(id);
            if (album == null) {
                throw new ArgumentException($"No Writer with ID:{id} was found");
            }
            return album;
        }

        public IQueryable<Writer> ReadAll() {
            return repo.ReadAll();
        }

        public void Update(Writer item) {
            repo.Update(item);
        }

        // Writer Non-CRUD
        // Top 10 oldest Writers
        // Get latest and oldest album
        // Top 10 (sort by Album count)
        // List album titles for a specific Writer

        public IEnumerable<Writer> Top10Oldest() {
            var list = repo.ReadAll();
            var top10 = list.OrderBy(t => t.Age);
            return top10;
        }

        public IEnumerable<Album> OldestLatestAlbums(int writerId) {
            var writer = repo.Read(writerId);
            if (writer == null) {
                throw new ArgumentException($"No Writer with {writerId} was found");
            }

            var sorted = writer.Albums.OrderBy(t => t.ReleaseYear);
            var albums = new List<Album> {
                sorted.First(),
                sorted.Last()
            }.AsEnumerable();

            return albums;
        }

        public IEnumerable<Album> ListAlbums(int writerId) {
            var writer = repo.Read(writerId);
            if (writer == null) {
                throw new ArgumentException($"No Writer with {writerId} was found");
            }

            var albums = writer.Albums;
            return albums;
        }

        public IEnumerable<Writer> Top10AlbumCount() {
            var list = repo.ReadAll();
            var top10 = list.OrderByDescending(t => t.Albums.Count)
                            .Take(10);

            return top10;
        }

        // 2 Tables
        public IEnumerable<Writer> WritersWithAlbumsInGenre(string genreString) {
            Genre genre = (Genre)Enum.Parse(typeof(Genre), genreString);
            var read = repo.ReadAll();
            var writers = read.Where(writer => writer.Albums.Any(album => album.Genre == genre))
                              .ToList();
            return writers;
        }
    }
}
