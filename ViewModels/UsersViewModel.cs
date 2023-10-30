using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestePontual.Context;

namespace TestePontual.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IList<string> Tipo { get; set; }
    }

   
 }