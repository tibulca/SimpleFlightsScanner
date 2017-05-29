using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsApp.Lib.Models
{
    public class Trip
    {
        public List<Flight> Flights { get; set; }

        public DateTime DateFrom 
        { 
            get
            {
                return Flights.First().DateFrom;    
            } 
        }

        //public double TotalPrice
        //{
        //    get
        //    {
        //        return Flights.Sum(f => f.Price);
        //    }
        //}

        public Trip(List<Flight> flights)
        {
            this.Flights = flights;
        }
    }
}
