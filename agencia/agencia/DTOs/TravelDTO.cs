using System;
using System.ComponentModel.DataAnnotations;
using agencia.Models;

namespace agencia.DTOs
{
    public class TravelDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "O destino é obrigatório")]
        [StringLength(200, ErrorMessage = "O destino não pode ter mais do que 200 caracteres")]
        public string Destination { get; set; }
        
        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
    
    public class TravelCreateDto
    {
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "O destino é obrigatório")]
        [StringLength(200, ErrorMessage = "O destino não pode ter mais do que 200 caracteres")]
        public string Destination { get; set; }
        
        [Required(ErrorMessage = "O cliente é obrigatório")]
        public int CustomerId { get; set; }
    }
}