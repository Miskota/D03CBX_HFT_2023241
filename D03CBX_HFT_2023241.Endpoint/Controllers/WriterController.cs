using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class WriterController : ControllerBase {

        IWriterLogic logic;
        public WriterController(IWriterLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<WriterController>
        [HttpGet]
        public IEnumerable<Writer> ReadAll() {
            return this.logic.ReadAll();
        }

        // GET api/<WriterController>/5
        [HttpGet("{id}")]
        public Writer Read(int id) {
            return this.logic.Read(id);
        }

        // POST api/<WriterController>
        [HttpPost]
        public void Create([FromBody] Writer value) {
            this.logic.Create(value);
        }

        // PUT api/<WriterController>/5
        [HttpPut]
        public void Update([FromBody] Writer value) {
            this.logic.Update(value);
        }

        // DELETE api/<WriterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            this.logic.Delete(id);
        }
    }
}
