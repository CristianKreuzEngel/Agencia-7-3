using System;

namespace agencia.Models
{
    public class Travel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Destination { get; set; }
        public Customer Customer { get; set; } // Cliente que irá viajar
    }
}