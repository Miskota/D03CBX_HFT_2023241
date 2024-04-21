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
    public class AlbumController : ControllerBase {

        IAlbumLogic logic;
        IHubContext<SignalRHub> hub;
        public AlbumController(IAlbumLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: api/<AlbumController>
        [HttpGet]
        public IEnumerable<Album> ReadAll() {
            return this.logic.ReadAll();
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public Album Read(int id) {
            return this.logic.Read(id);
        }

        // POST api/<AlbumController>
        [HttpPost]
        public void Create([FromBody] Album value) {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("AlbumCreated", value);
        }

        // PUT api/<AlbumController>/5
        [HttpPut]
        public void Update([FromBody] Album value) {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("AlbumUpdated", value);
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            var albumToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("AlbumDeleted", albumToDelete);
        }
    }
}
