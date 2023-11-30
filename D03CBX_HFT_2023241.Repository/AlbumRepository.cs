using D03CBX_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Repository {
    public class AlbumRepository : Repository<Album> {
        public AlbumRepository(MusicDBContext ctx) : base(ctx)
        {
            
        }

        public override Album Read(int id) {
            return ctx.Albums.FirstOrDefault(h => h.AlbumID == id);
        }

        public override void Update(Album item) {
            var old = Read(item.AlbumID);
            foreach (var prop in old.GetType().GetProperties()) {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null) {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
