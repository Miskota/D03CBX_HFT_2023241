using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Repository {
    public abstract class Repository<T> : IRepository<T> where T : class {
        protected MusicDBContext ctx;

        public Repository(MusicDBContext ctx) {
            this.ctx = ctx;
        }

        public void Create(T item) {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public IQueryable ReadAll() {
            return ctx.Set<T>();
        }

        public void Delete(int id) {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
