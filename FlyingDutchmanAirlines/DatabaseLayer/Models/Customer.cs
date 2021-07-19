using System;
using System.Collections.Generic;

#nullable disable

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public sealed class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Bookings = new HashSet<Booking>();
        }

        public int CustomerId { get; set; }
        public string Name { get; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
