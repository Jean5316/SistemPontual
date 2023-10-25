using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using SQLitePCL;
using TestePontual.Context;
using TestePontual.Models;
using TestePontual.Repository;


namespace TestePontual.Repositories
{
    public class ClienteRepositories : IClienteRepository
    {
        //injeção de instancia da classe de context para acessar os dados
        private readonly ClienteContext _context;

        public ClienteRepositories(ClienteContext context)
        {
            _context = context;
        }

        //implementando propriedade clientes que retorna uma lista de clientes
        public IEnumerable<Cliente> Clientes => _context.Clientes;

       
    }
}