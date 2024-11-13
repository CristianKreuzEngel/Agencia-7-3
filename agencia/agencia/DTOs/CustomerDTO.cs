using System.Collections.Generic;

namespace agencia.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Preferences { get; set; }
    }
}
