using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class RecordController : ControllerBase {
        // GET: api/<RecordController>

        IRecordLogic logic;

        public RecordController(IRecordLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Record> ReadAll() {
            return this.logic.ReadAll();
        }

        // GET api/<RecordController>/5
        [HttpGet("{id}")]
        public Record Read(int id) {
            return this.logic.Read(id);
        }

        // POST api/<RecordController>
        [HttpPost]
        public void Create([FromBody] Record value) {
            this.logic.Create(value);
        }

        // PUT api/<RecordController>/5
        [HttpPut]
        public void Update([FromBody] Record value) {
            this.logic.Update(value);
        }

        // DELETE api/<RecordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            this.logic.Delete(id);
        }
    }
}
