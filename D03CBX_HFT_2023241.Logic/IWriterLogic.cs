using D03CBX_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace D03CBX_HFT_2023241.Logic {
    public interface IWriterLogic {
        void Create(Writer item);
        void Delete(int id);
        IEnumerable<Album> ListAlbums(int writerId);
        IEnumerable<Album> OldestLatestAlbums(int writerId);
        Writer Read(int id);
        IQueryable<Writer> ReadAll();
        IEnumerable<Writer> Top10AlbumCount();
        void Update(Writer item);
        IEnumerable<Writer> WritersWithAlbumsInGenre(string genreString);
    }
}