using D03CBX_HFT_2023241.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Repository {
    class RecordRepository : Repository<Record> {

        public RecordRepository(MusicDBContext ctx) : base(ctx)
        {
        }

        public override Record Read(int id) {
            return ctx.Records.FirstOrDefault(h => h.RecordID == id);
        }

        public override void Update(Record item) {
            var old = Read(item.RecordID);
            foreach (var prop in old.GetType().GetProperties()) {
                // Virtual property is no-go
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null) {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
