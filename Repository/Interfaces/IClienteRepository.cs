using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePontual.Models;

namespace TestePontual.Repository
{
    public interface IClienteRepository
    {
        //propriedade
        public IEnumerable<Cliente> Clientes { get; }


        // // //metodo
        Cliente GetClientById(int clienteId);

        public void CriarCliente(Cliente cliente);
        
        public void EditarCliente(Cliente cliente);

        public void DeletarCliente(Cliente cliente);

        Cliente Detalhes(int cliente);

        

        
        
        
        




    }
}