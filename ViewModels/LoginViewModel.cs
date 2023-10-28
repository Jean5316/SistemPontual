using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestePontual.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Usuario")]
        [Display(Name = "Usuarios")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} e no maximo {1} characteres", MinimumLength = 6)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

       
        public string ReturnUrl { get; set; }
    }

}