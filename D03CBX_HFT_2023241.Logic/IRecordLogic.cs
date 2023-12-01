using D03CBX_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace D03CBX_HFT_2023241.Logic {
    public interface IRecordLogic {
        void Create(Record item);
        void Delete(int id);
        IEnumerable<GenreStatistics> GenreStatistics();
        IEnumerable<Record> ListByGenre(string genreString);
        Record Read(int id);
        IQueryable<Record> ReadAll();
        IEnumerable<Record> Top10Plays();
        IEnumerable<Record> Top10Rated();
        void Update(Record item);
    }
}