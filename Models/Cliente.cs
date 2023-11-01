using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestePontual.Context;

namespace TestePontual.Models
{
    [Table("Clientes")]//define nome da tebela no banco de dados
    public class Cliente
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatorio!")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O Campo deve conter 4 a 50 Caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Numero Telefone")]
        [DisplayFormat(DataFormatString = "(67)98134-2083")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "O Campo deve ser preenchido com numero de celular ou telefone fixo")]
        public string Contato { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um email valido")]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "79004-310")]
        public string Cep { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Rua { get; set; }

        [Range(1, 9999, ErrorMessage = "Excedeu o valor maximo do campo!")]
        public int Numero { get; set; }


        public string Complemento { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Bairro { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Cidade { get; set; }

        [StringLength(25, MinimumLength = 4)]
        public string Estado { get; set; }




    }
}