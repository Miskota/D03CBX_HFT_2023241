using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Models {
    public enum Genre {
        Classic, Jazz, Country, Pop, Rock, Metal, Electro, Punk, Folk, Disco, Funk, Synth, HipHop
    }
    public class Album {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumID { get; set; }

        [ForeignKey(nameof(Writer))]
        public int WriterID { get; set; }

        public Album() {
            Records = new List<Record>();
        }

        [StringLength(240)]
        public string AlbumName { get; set; }
        public Genre Genre { get; set; }
        public int ReleaseYear { get; set; }

        [NotMapped]
        public virtual Writer Writer { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Record> Records { get; set; }


        public Album(string line)
        {
            string[] split = line.Split('#');
            AlbumID = int.Parse(split[0]);
            WriterID = int.Parse(split[1]);
            AlbumName = split[2];
            Genre = (Genre)Enum.Parse(typeof(Genre), split[3]);
            ReleaseYear = int.Parse(split[4]);
        }
    }
}
