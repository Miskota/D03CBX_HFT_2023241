using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D03CBX_HFT_2023241.Endpoint.Controllers {
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase {

        IAlbumLogic albumLogic;
        IWriterLogic writerLogic;
        IRecordLogic recordLogic;

        public NonCrudController(IAlbumLogic albumLogic, IWriterLogic writerLogic, IRecordLogic recordLogic)
        {
            this.albumLogic = albumLogic;
            this.writerLogic = writerLogic;
            this.recordLogic = recordLogic;
        }

        // Writer
        [HttpGet("{genre}")]
        public IEnumerable<Writer> WritersWithAlbumsInGenre(string genre) {
            return writerLogic.WritersWithAlbumsInGenre(genre);
        }

        [HttpGet("{writerId}")]
        public IEnumerable<Album> OldestLatestAlbums(int writerId) {
            return writerLogic.OldestLatestAlbums(writerId);
        }

        [HttpGet("{writerId}")]
        public IEnumerable<Album> ListAlbums(int writerId) {
            return writerLogic.ListAlbums(writerId);
        }

        [HttpGet]
        public IEnumerable<Writer> Top10AlbumCount() {
            return writerLogic.Top10AlbumCount();
        }



        // Album
        [HttpGet("{year}")]
        public IEnumerable<string> ListByYear(int year) {
            return albumLogic.ListByYear(year);
        }

        [HttpGet]
        public IEnumerable<string> AveragePlaysAlbum() {
            return albumLogic.AveragePlaysAlbum();
        }



        // Record
        [HttpGet]
        public IEnumerable<string> GenreStatistics() {
            return recordLogic.GenreStatistics();
        }

        [HttpGet("{genre}")]
        public IEnumerable<Record> ListByGenre(string genre) {
            return recordLogic.ListByGenre(genre);
        }

        [HttpGet]
        public IEnumerable<Record> Top10Plays() {
            return recordLogic.Top10Plays();
        }

        [HttpGet]
        public IEnumerable<Record> Top10Rated() {
            return recordLogic.Top10Rated();
        }


        //// GET: api/<NonCrudController>
        //[HttpGet]
        //public IEnumerable<string> Get() {
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<NonCrudController>/5
        //[HttpGet("{id}")]
        //public string Get(int id) {
        //    return "value";
        //}

        //// POST api/<NonCrudController>
        //[HttpPost]
        //public void Post([FromBody] string value) {
        //}

        //// PUT api/<NonCrudController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value) {
        //}

        //// DELETE api/<NonCrudController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
    }
}
