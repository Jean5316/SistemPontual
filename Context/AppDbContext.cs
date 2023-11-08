using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestePontual.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestePontual.Areas.Admin.Models;


namespace TestePontual.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; } //define tabela no banco de dados nome Clientes
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<OrdemServico> OS { get; set; }
    }
}