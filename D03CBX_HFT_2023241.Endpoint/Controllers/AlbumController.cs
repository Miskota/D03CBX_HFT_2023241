using D03CBX_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase {

        AlbumLogic logic;


        // GET: api/<AlbumController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<AlbumController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
