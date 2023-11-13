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


        [StringLength(240)]
        public virtual Album Album { get; set; }


        public int Plays { get; set; }


        [Range(1.0, 5.0)]
        public double Rating { get; set; }


        public int Duration { get; set; }


        public Genre Genre { get; set; }
    }
}
