using System.Collections.Generic;

namespace agencia.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Preferences { get; set; }
    }
}