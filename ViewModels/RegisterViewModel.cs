using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestePontual.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe o Usuario")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um email valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} e no maximo {1} characteres", MinimumLength = 6)]
        [Display(Name = "Senha Atual")]
        public string currentPassword { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} e no maximo {1} characteres", MinimumLength = 6)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha de Confirmação")]
        [Compare("Password", ErrorMessage = "A Senha deve ser igual!")]
        public string ConfirmPassword { get; set; }


        public string ReturnUrl { get; set; }


    }

    public class EditarUsuarioViewModel
    {
        
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um email valido")] 
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} e no maximo {1} characteres", MinimumLength = 6)]
        [Display(Name = "Senha Atual")]
        public string currentPassword { get; set; }

        
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} e no maximo {1} characteres", MinimumLength = 6)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha de Confirmação")]
        [Compare("Password", ErrorMessage = "A Senha deve ser igual!")]
        public string ConfirmPassword { get; set; }
    }
}