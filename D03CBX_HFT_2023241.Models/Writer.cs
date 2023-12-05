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
        public string Age { get { 
                if (YearOfBirth == -1) {
                    return "Undefined";
                }
                return (DateTime.Now.Year - YearOfBirth).ToString(); } }

        [StringLength(240)]
        public string WriterName { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Album> Albums { get; set; }

        public Writer(string line)
        {
            string[] split = line.Split('#');
            WriterID = int.Parse(split[0]);
            YearOfBirth = int.Parse(split[1]);
            WriterName = split[2];
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            Writer a = (Writer)obj;

            // Matching ID is enough
            return WriterID == a.WriterID;
        }
    }
}
