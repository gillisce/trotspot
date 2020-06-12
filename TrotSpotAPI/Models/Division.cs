using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models
{
    public class Division
    {
        public int ID { get; set; }
        public int ShowID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MaxAge { get; set; }
        public int? MinAge { get; set; }
        public int? MaxNumRiders { get; set; }
        public int? MinNumRiders { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Creator { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Modifier { get; set; }
    }
}
