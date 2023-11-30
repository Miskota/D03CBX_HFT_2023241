using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Logic {
    public class RecordLogic {
        IRepository<Record> repo;

        public RecordLogic(IRepository<Record> repo) {
            this.repo = repo;
        }
        public void Create(Record item) {
            repo.Create(item);
        }

        public void Delete(int id) {
            if (repo.Read(id) == null) {
                throw new ArgumentException($"No Record with ID:{id} was found");
            }
            repo.Delete(id);
        }

        public Record Read(int id) {
            var album = repo.Read(id);
            if (album == null) {
                throw new ArgumentException($"No Record with ID:{id} was found");
            }
            return album;
        }

        public IQueryable<Record> ReadAll() {
            return repo.ReadAll();
        }

        public void Update(Record item) {
            repo.Update(item);
        }

        // Record Non-CRUD
        // Most plays (Top 10?)
        // List by genre
        // Top 10 rated songs
        // Group by genre, statistics
        // Non crud with multiple tables (5)

        public IEnumerable<Record> Top10Plays() {
            var list = repo.ReadAll().ToList();
            var top10 = list.OrderByDescending(t => t.Plays)
                            .Take(10);
                            
            return top10;
        }

        public IEnumerable<Record> Top10Rated() {
            var list = repo.ReadAll().ToList();
            var top10 = list.OrderByDescending(t => t.Rating)
                            .Take(10);
                            
            return top10;
        }

        public IEnumerable<Record> ListByGenre(string genreString) {
            Genre genre = (Genre)Enum.Parse(typeof(Genre), genreString);
            var list = repo.ReadAll()
                           .Where(t => t.Genre == genre);
                           

            if (list == null) {
                throw new ArgumentException($"No music with {genreString} was found");
            }
            return list;
        }

        public IEnumerable<GenreStatistics> GenreStatistics() {
            var list = repo.ReadAll().ToList();
            var grouped = from x in list
                          group x by x.Genre into g
                          select new GenreStatistics {
                              Genre = g.Key.ToString(),
                              AveragePlay = g.Average(t => t.Plays),
                              AverageRating = g.Average(t => t.Rating),
                              AverageLength = g.Average(t => t.Duration)
                          };
            return grouped;
        }
    }
}
