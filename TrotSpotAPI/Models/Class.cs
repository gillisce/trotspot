using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models
{
    public class Class
    {
        public int ID { get; set; }
        public int ClassNumber { get; set; }
        public string ClassName { get; set; }
        public float? Cost { get; set; }
        public DateTime? ClassStart { get; set; }
        public int? MaxRiderCount { get; set; }
        public int MinRiderCount { get; set; }
        public bool ActiveClass { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public string ClassNotes { get; set; }
        public int ShowID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Creator { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Modifier { get; set; }
    }
}
