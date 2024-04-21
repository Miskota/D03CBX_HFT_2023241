using D03CBX_HFT_2023241.Endpoint.Services;
using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class RecordController : ControllerBase {
        // GET: api/<RecordController>

        IRecordLogic logic;
        IHubContext<SignalRHub> hub;

        public RecordController(IRecordLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("RecordCreated", value);
        }

        // PUT api/<RecordController>/5
        [HttpPut]
        public void Update([FromBody] Record value) {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RecordUpdated", value);
        }

        // DELETE api/<RecordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            var recordToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RecordDeleted", recordToDelete);
        }
    }
}
