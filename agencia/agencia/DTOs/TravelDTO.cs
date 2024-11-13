using System;

namespace agencia.DTOs
{
    public class TravelDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Destination { get; set; }
        public int CustomerId { get; set; }
    }
}
