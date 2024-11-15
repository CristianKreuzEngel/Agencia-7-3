using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using agencia.Models;

namespace agencia.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais do que 100 caracteres")]
        public string Name { get; set; }
        
        public List<Tag> Preferences { get; set; } = new List<Tag>();
    }
    
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais do que 100 caracteres")]
        public string Name { get; set; }
        
        public List<string> Preferences { get; set; } = new List<string>();
    }
    
    public class CustomerUpdateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais do que 100 caracteres")]
        public string Name { get; set; }
        
        public List<string> Preferences { get; set; } = new List<string>();
    }
}