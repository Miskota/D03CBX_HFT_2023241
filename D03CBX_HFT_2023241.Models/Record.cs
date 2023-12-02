using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Models {
    public class Record {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordID { get; set; }

        [ForeignKey(nameof(Album))]
        public int AlbumID { get; set; }

        [StringLength(240)]
        public string Title { get; set; }


        
        [NotMapped]
        public virtual Album Album { get; set; }


        public int Plays { get; set; }


        [Range(1.0, 5.0)]
        public double Rating { get; set; }


        public int Duration { get; set; }


        public Genre Genre { get; set; }

        public Record() {
            
        }

        public Record(string line)
        {
            string[] split = line.Split('#');
            RecordID = int.Parse(split[0]);
            AlbumID = int.Parse(split[1]);
            Title = split[2];
            Plays = int.Parse(split[3]);
            Duration = int.Parse(split[4]);
            Genre = (Genre)Enum.Parse(typeof(Genre), split[5]);
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            Record otherRecord = (Record)obj;

            return RecordID == otherRecord.RecordID &&
                   AlbumID == otherRecord.AlbumID &&
                   string.Equals(Title, otherRecord.Title) &&
                   Plays == otherRecord.Plays &&
                   Duration == otherRecord.Duration &&
                   Genre == otherRecord.Genre;
        }
    }
}
