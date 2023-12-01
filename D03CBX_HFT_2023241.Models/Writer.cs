using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Models {
    public class Writer {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WriterID { get; set; }

        public Writer() {
            Albums = new List<Album>();
        }
        public int YearOfBirth { get; set; }

        [NotMapped]
        public int Age { get { return DateTime.Now.Year - YearOfBirth; } }

        [StringLength(240)]
        public string WriterName { get; set; }

        //[JsonIgnore]
        [NotMapped]
        public virtual ICollection<Album> Albums { get; set; }

    }
}
