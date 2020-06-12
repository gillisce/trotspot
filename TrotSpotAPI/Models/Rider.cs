using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models
{
    public class Rider
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public float AmountDue { get; set; }
        public bool HasPaid { get; set; }
        public string PaymentMethod { get; set; }
        public bool Active { get; set; }

    }
}
