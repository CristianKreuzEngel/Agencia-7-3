using System;
using System.ComponentModel.DataAnnotations;

namespace agencia.Models
{
    public class Travel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "O destino é obrigatório")]
        [StringLength(200, ErrorMessage = "O destino não pode ter mais do que 200 caracteres")]
        public string Destination { get; set; }
        
        [Required(ErrorMessage = "O cliente é obrigatório")]
        public Customer Customer { get; set; }
    }
}