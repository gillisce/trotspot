using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models
{
    public class Show
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxRiderNumber { get; set; }
        public DateTime? ShowDate { get; set; }
        public DateTime? RegistrationStart { get; set; }
        public DateTime? RegistrationEnd { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? ZipCode { get; set; }
       
    }
}

