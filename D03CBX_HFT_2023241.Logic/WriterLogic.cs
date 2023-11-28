using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Logic {
    public class WriterLogic {
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
    }
}
