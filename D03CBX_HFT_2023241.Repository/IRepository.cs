using D03CBX_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Repository {
    public interface IRepository {
        void Create(Record record);
        Record Read(int id);
        void Update(Record record);
        void Delete(int id);

        IQueryable<Record> ReadAll();
    }
}
