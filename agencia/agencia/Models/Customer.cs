using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace agencia.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais do que 100 caracteres")]
        public string Name { get; set; }
        
        public List<Tag> Preferences { get; set; } = new List<Tag>();
    }
}