using D03CBX_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace D03CBX_HFT_2023241.Logic {
    public interface IAlbumLogic {
        int AlbumCount();
        void Create(Album item);
        void Delete(int id);
        IEnumerable<string> ListByYear(int year);
        Album Read(int id);
        IQueryable<Album> ReadAll();
        void Update(Album item);
        IEnumerable<string> AveragePlaysAlbum();
    }
}