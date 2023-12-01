using D03CBX_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Repository {
    public class WriterRepository : Repository<Writer> {

        public WriterRepository(MusicDBContext ctx) : base(ctx)
        {
            
        }

        public override Writer Read(int id) {
            return ctx.Writers.FirstOrDefault(h => h.WriterID == id);
        }

        public override void Update(Writer item) {
            var old = Read(item.WriterID);
            foreach (var prop in old.GetType().GetProperties()) {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null) {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
