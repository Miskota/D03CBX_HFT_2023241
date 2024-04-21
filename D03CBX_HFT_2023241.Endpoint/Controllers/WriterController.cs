using D03CBX_HFT_2023241.Endpoint.Services;
using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class WriterController : ControllerBase {

        IWriterLogic logic;
        IHubContext<SignalRHub> hub;
        public WriterController(IWriterLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("WriterCreated", value);
        }

        // PUT api/<WriterController>/5
        [HttpPut]
        public void Update([FromBody] Writer value) {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("WriterUpdated", value);
        }

        // DELETE api/<WriterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            var writerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("WriterDeleted", writerToDelete);
        }
    }
}
