using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePontual.Areas.Admin.Models
{
    public class Produto
    {
        [Display(Name = "Cod.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Valor de Estoque Obrigatório")]

        [Display(Name = "Estoque Atual")]
        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]

        public decimal Preco { get; set; }
    }
}