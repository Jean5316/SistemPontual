using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestePontual.Models
{
    public class OrdemServico
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Voce deve informar o cliente!")]
        [Display(Name = "Cliente")]
        [StringLength(100)]
        public string NomeCliente { get; set; }
        
        [Required(ErrorMessage = "Voce deve informar o tecnico responsavel!")]
        [Display(Name = "Técnico Responsavel")]
        [StringLength(100)]
        public string Tecnico { get; set; }
        
        [Required]
        public EnumStatus Status { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A descrição deve ser informada")]
        [Display(Name = "Descrição")]
        [MinLength(10, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada pode exceder {1} caracteres")]
        public string Descricao { get; set; }

        
        public int ClienteId { get; set; }//Chave extrangeira na tabela clientes
    }
}